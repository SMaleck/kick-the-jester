﻿using Assets.Source.Mvc.Data;
using Assets.Source.Mvc.Views.PartialViews;
using Assets.Source.Services;
using Assets.Source.Util;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Source.Services.Localization;
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

        [Header("Tomatoes")]
        [SerializeField] Image _projectileIcon;
        [SerializeField] TextMeshProUGUI _projectileAmountText;

        [Header("Money Gain Floating Numbers")]
        [SerializeField] RectTransform _pickupFeedbackParent;

        [Header("Other")]
        [SerializeField] Button _pauseButton;
        [SerializeField] Slider _kickForceBar;

        [Header("Velocity Display")]
        [SerializeField] private RectTransform _velocityArrow;
        [SerializeField] private float _velocityMinZRotation;
        [SerializeField] private float _velocityMaxZRotation;

        [Header("Out of Camera Indicator")]
        [SerializeField] RectTransform _outOfCameraIndicator;
        [SerializeField] float _indicatorAnimStrength = 2;
        [SerializeField] float _indicatorAnimSpeedSeconds = 0.8f;

        [Header("HUD Parts")]
        [SerializeField] RectTransform _bottomHUDPreFlightParent;
        [SerializeField] RectTransform _bottomHUDFlightParent;

        private readonly ReactiveCommand _onPauseButtonClicked = new ReactiveCommand();
        public IObservable<Unit> OnPauseButtonClicked => _onPauseButtonClicked;

        private ViewPrefabConfig _viewPrefabConfig;
        private PickupFeedbackView.Factory _pickupFeedbackViewFactory;
        private List<PickupFeedbackView> _pickupFeedbackViews;

        private readonly Color _projectileIconInactiveColor = new Color(1, 1, 1, 0.5f);
        private Tween _outOfCameraIndicatorTween;

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

            _outOfCameraIndicatorTween = CreateOutOfCameraIndicatorTween();
            SetOutOfCameraIndicatorVisible(false);

            _bottomHUDFlightParent.anchoredPosition = new Vector2(
                _bottomHUDFlightParent.anchoredPosition.x,
                -_bottomHUDFlightParent.rect.height);

            UpdateTexts();
        }

        private void UpdateTexts()
        {
            _bestDistanceLabelText.text = TextService.BestLabel();
        }

        public void SetDistance(float value)
        {
            _distanceText.text = TextService.MetersAmount(value);
        }

        public void SetBestDistance(float value)
        {
            _bestDistanceText.text = TextService.MetersAmount(value);
        }

        public void SetRelativeKickForce(float value)
        {
            _kickForceBar.value = value;
        }

        public void SetRelativeVelocity(float value)
        {
            var rotationZ = Mathf.Lerp(_velocityMinZRotation, _velocityMaxZRotation, value);
            _velocityArrow.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }

        public void SetOutOfCameraIndicatorVisible(bool value)
        {
            _outOfCameraIndicator.gameObject.SetActive(value);

            if (value)
            {
                _outOfCameraIndicatorTween.Play();
                return;
            }

            _outOfCameraIndicatorTween.Pause();
        }

        public void SetCurrencyAmount(int amount)
        {
            _currencyAmountText.text = TextService.CurrencyAmount(amount);
        }

        public void SetCollectedCurrencyAmount(int amount)
        {
            _collectedCurrencyAmountText.text = TextService.CurrencyAmount(amount);
        }

        public void SetProjectileAmount(int amount)
        {
            _projectileAmountText.text = TextService.TimesAmount(amount);

            var isOutOfProjectiles = amount <= 0;

            _projectileAmountText.gameObject.SetActive(!isOutOfProjectiles);
            _projectileIcon.color = isOutOfProjectiles
                ? _projectileIconInactiveColor
                : Color.white;
        }

        private Tween CreateOutOfCameraIndicatorTween()
        {
            var start = _outOfCameraIndicator.anchoredPosition.y;
            var end = start - _indicatorAnimStrength;

            return DOTween.To(
                    (value) =>
                    {
                        _outOfCameraIndicator.anchoredPosition =
                            new Vector2(_outOfCameraIndicator.anchoredPosition.x, value);
                    },
                    start, end, _indicatorAnimSpeedSeconds)
                .SetEase(Ease.InOutCubic)
                .SetLoops(-1, LoopType.Yoyo)
                .SetAutoKill(false)
                .Pause()
                .AddTo(Disposer, TweenDisposalBehaviour.Rewind);
        }

        public void StartRound()
        {
            DOTween.Sequence()
                .Append(_bottomHUDPreFlightParent.DOAnchorPosY(-_bottomHUDPreFlightParent.rect.height, 0.5f))
                .Append(_bottomHUDFlightParent.DOAnchorPosY(0, 1)
                    .SetEase(Ease.OutCubic));
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

            if (freeSlot == null)
            {
                freeSlot = _pickupFeedbackViewFactory
                    .Create(_viewPrefabConfig.PickupFeedbackViewPrefab);

                freeSlot.transform.SetParent(_pickupFeedbackParent, false);
                freeSlot.Initialize();

                _pickupFeedbackViews.Add(freeSlot);
            }

            return freeSlot;
        }
    }
}
