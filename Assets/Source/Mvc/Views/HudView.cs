﻿using Assets.Source.App;
using Assets.Source.Util;
using Assets.Source.Util.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class HudView : AbstractView
    {
        [Header("Flight Stats Display")]
        [SerializeField] TMP_Text _distanceText;
        [SerializeField] TMP_Text _heightText;
        [SerializeField] TMP_Text _bestDistanceText;

        [Header("Tomatoes")]
        [SerializeField] GameObject _shotCountPanel;
        [SerializeField] GameObject _pfShotCountIcon;
        private List<Image> shotCountIcons = new List<Image>();

        [Header("Money Gain Floating Numbers")]
        [SerializeField] RectTransform _moneyGainPanel;
        [SerializeField] GameObject _pfMoneyGainText;
        private List<FloatingValue> moneyGainSlots = new List<FloatingValue>();

        [Header("Other")]
        [SerializeField] Button _pauseButton;
        [SerializeField] UIProgressBar _velocityBar;
        [SerializeField] UIProgressBar _kickForceBar;

        [Header("Out of Camera Indicator")]
        [SerializeField] RectTransform _outOfCameraIndicator;
        [SerializeField] float _indicatorAnimStrength = 2;
        [SerializeField] float _indicatorAnimSpeedSeconds = 0.8f;


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

        public ReactiveCommand OnPauseButtonClicked = new ReactiveCommand();


        public override void Setup()
        {
            _pauseButton.OnClickAsObservable()
                       .Subscribe(_ => OnPauseButtonClicked.Execute())
                       .AddTo(this);


            _velocityBar.gameObject.SetActive(false);
            _kickForceBar.gameObject.SetActive(true);
            _shotCountPanel.gameObject.SetActive(false);

            SetupIndicatorTweener();
        }

        private void SetupIndicatorTweener()
        {
            OutOfCameraIndicatorVisible = false;

            var start = _outOfCameraIndicator.anchoredPosition.y;
            var end = start - _indicatorAnimStrength;

            DOTween.To((value) =>
                {
                    _outOfCameraIndicator.anchoredPosition = new Vector2(_outOfCameraIndicator.anchoredPosition.x, value);
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

        public void ShowFloatingCoinAmount(float gainedAmount)
        {
            if (gainedAmount <= 0) { return; }

            FloatingValue fValue = moneyGainSlots.FirstOrDefault(e => !e.IsFloating);

            if (fValue == null)
            {
                GameObject go = GameObject.Instantiate(_pfMoneyGainText, _moneyGainPanel, false);
                moneyGainSlots.Add(go.GetComponent<FloatingValue>());

                fValue = moneyGainSlots.Last();
            }

            fValue.StartFloating($"{gainedAmount} {Constants.TMP_SPRITE_COIN}");
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
