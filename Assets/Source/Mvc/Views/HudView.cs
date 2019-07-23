using Assets.Source.Mvc.Data;
using Assets.Source.Mvc.Views.PartialViews;
using Assets.Source.Services;
using Assets.Source.Util;
using Assets.Source.Util.UI;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Source.Mvc.Views
{
    public class HudView : AbstractView
    {
        [Header("Flight Stats Display")]
        [SerializeField] TextMeshProUGUI _currencyAmountText;
        [SerializeField] TextMeshProUGUI _collectedCurrencyAmountText;
        [SerializeField] TextMeshProUGUI _distanceText;
        [SerializeField] TextMeshProUGUI _bestDistanceLabelText;
        [SerializeField] TextMeshProUGUI _bestDistanceText;
        [SerializeField] TextMeshProUGUI _heightText;

        [Header("Tomatoes")]
        [SerializeField] GameObject _shotCountPanel;
        [SerializeField] GameObject _pfShotCountIcon;
        private List<Image> shotCountIcons = new List<Image>();

        [Header("Money Gain Floating Numbers")]
        [SerializeField] RectTransform _pickupFeedbackParent;        

        [Header("Other")]
        [SerializeField] Button _pauseButton;
        [SerializeField] UIProgressBar _velocityBar;
        [SerializeField] UIProgressBar _kickForceBar;

        [Header("Out of Camera Indicator")]
        [SerializeField] RectTransform _outOfCameraIndicator;
        [SerializeField] float _indicatorAnimStrength = 2;
        [SerializeField] float _indicatorAnimSpeedSeconds = 0.8f;

        // ToDo change to setter methods
        public float Distance
        {
            set { _distanceText.text = value.ToMetersString(); }
        }

        public float Height
        {
            set { _heightText.text = value.ToMetersString(); }
        }

        public float BestDistance
        {
            set { _bestDistanceText.text = value.ToMetersString(); }
        }

        public float RelativeVelocity
        {
            set { _velocityBar.fillAmount = value; }
        }

        public float RelativeKickForce
        {
            set { _kickForceBar.fillAmount = value; }
        }

        public bool OutOfCameraIndicatorVisible
        {
            set { _outOfCameraIndicator.gameObject.SetActive(value); }
        }

        private ReactiveCommand _onPauseButtonClicked = new ReactiveCommand();
        public IObservable<Unit> OnPauseButtonClicked => _onPauseButtonClicked;

        private ViewPrefabConfig _viewPrefabConfig;
        private PickupFeedbackView.Factory _pickupFeedbackViewFactory;

        private List<PickupFeedbackView> _pickupFeedbackViews;

        [Inject]
        private void Inject(
            ViewPrefabConfig viewPrefabConfig,
            PickupFeedbackView.Factory pickupFeedbackViewFactory)
        {
            _viewPrefabConfig = viewPrefabConfig;
            _pickupFeedbackViewFactory = pickupFeedbackViewFactory;
        }

        public override void Setup()
        {
            _pickupFeedbackViews = new List<PickupFeedbackView>();

            _onPauseButtonClicked.AddTo(Disposer);
            _onPauseButtonClicked.BindTo(_pauseButton).AddTo(Disposer);

            _velocityBar.gameObject.SetActive(false);
            _kickForceBar.gameObject.SetActive(true);
            _shotCountPanel.gameObject.SetActive(false);

            SetupIndicatorTweener();
            UpdateTexts();
        }

        public void SetCurrencyAmount(int amount)
        {
            _currencyAmountText.text = TextService.CurrencyAmount(amount);
        }

        public void SetCollectedCurrencyAmount(int amount)
        {
            _collectedCurrencyAmountText.text = TextService.CurrencyAmount(amount);
        }

        private void UpdateTexts()
        {
            _bestDistanceLabelText.text = TextService.BestLabel();
        }

        // ToDo Improve this Tween
        private void SetupIndicatorTweener()
        {
            OutOfCameraIndicatorVisible = false;

            var start = _outOfCameraIndicator.anchoredPosition.y;
            var end = start - _indicatorAnimStrength;

            DOTween.To(
                    (value) =>
                    {
                        _outOfCameraIndicator.anchoredPosition =
                            new Vector2(_outOfCameraIndicator.anchoredPosition.x, value);
                    },
                    start, end, _indicatorAnimSpeedSeconds)
                .SetEase(Ease.OutBounce)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void StartRound()
        {
            _velocityBar.gameObject.SetActive(true);
            _kickForceBar.gameObject.SetActive(false);
            _shotCountPanel.gameObject.SetActive(true);
        }
        
        public void ShowFloatingCoinAmount(int gainedAmount)
        {
            if (gainedAmount <= 0)
            {
                return;
            }

            var freeSlot = GetFreePickupFeedbackSlot();
            freeSlot.SetCurrencyAmountWithAnimation(gainedAmount);
        }

        private PickupFeedbackView GetFreePickupFeedbackSlot()
        {
            var freeSlot = _pickupFeedbackViews
                .FirstOrDefault(view => !view.IsPlaying);

            if(freeSlot == null)
            {
                freeSlot = _pickupFeedbackViewFactory
                    .Create(_viewPrefabConfig.PickupFeedbackViewPrefab);

                freeSlot.transform.SetParent(_pickupFeedbackParent, false);
                freeSlot.Initialize();
            }

            return freeSlot;
        }

        public void OnShotCountChanged(int count)
        {
            int diff = Mathf.Abs(count - shotCountIcons.Count);

            // Add additional Icons, if the count is higher than the current amount
            if (shotCountIcons.Count < count)
            {
                AddShotCountIcons(diff);
                return;
            }

            // Reduce opacity if the count is lower
            var toDeactivate = shotCountIcons.Skip(count).Take(diff);

            foreach (Image img in toDeactivate)
            {
                img.color = new Color(1, 1, 1, 0.5f);
            }
        }

        private void AddShotCountIcons(int countToAdd)
        {
            float width = 0;

            for (int i = 0; i < countToAdd; i++)
            {
                var go = GameObject.Instantiate(_pfShotCountIcon);
                go.transform.SetParent(_shotCountPanel.transform);

                // Get the width if we did not do it yet, because we cannot get it reliably from the prefab
                if (width <= 0)
                {
                    width = go.GetComponent<RectTransform>().rect.width;
                }

                go.transform.localPosition = new Vector3(i * width, 0, 0);
                go.transform.localScale = Vector3.one;

                shotCountIcons.Add(go.GetComponent<Image>());
            }

            // Set all to full Opacity
            foreach (Image img in shotCountIcons)
            {
                img.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
