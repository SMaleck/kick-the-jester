using Assets.Source.Mvc.Data;
using Assets.Source.Mvc.Models.Enum;
using Assets.Source.Mvc.Views.PartialViews;
using Assets.Source.Services;
using Assets.Source.Util;
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
    public class RoundEndView : ClosableView
    {
        [Header("Labels")]
        [SerializeField] TextMeshProUGUI _distanceReachedText;
        [SerializeField] TextMeshProUGUI _newBestText;

        [Header("Distance Results")]
        [SerializeField] TextMeshProUGUI _distanceText;
        [SerializeField] TextMeshProUGUI _bestDistanceText;

        [Header("Currency Results")]
        [SerializeField] RectTransform _currencyGainsLayoutParent;
        [SerializeField] TextMeshProUGUI _currencyText;

        [Header("Buttons")]
        [SerializeField] Button _retryButton;
        [SerializeField] Button _shopButton;

        private readonly ReactiveCommand _onRetryClicked = new ReactiveCommand();
        public IObservable<Unit> OnRetryClicked => _onRetryClicked;

        private readonly ReactiveCommand _onUpgradesClicked = new ReactiveCommand();
        public IObservable<Unit> OnUpgradesClicked => _onUpgradesClicked;

        private const float CurrencyCounterSeconds = 1f;

        private ViewPrefabConfig _viewPrefabConfig;
        private CurrencyGainItemView.Factory _roundEarningsItemViewFactory;

        [Inject]
        private void Inject(
            ViewPrefabConfig viewPrefabConfig,
            CurrencyGainItemView.Factory roundEarningsItemViewFactory)
        {
            _viewPrefabConfig = viewPrefabConfig;
            _roundEarningsItemViewFactory = roundEarningsItemViewFactory;
        }

        public override void Setup()
        {
            base.Setup();

            _onRetryClicked.AddTo(Disposer);
            _onRetryClicked.BindTo(_retryButton).AddTo(Disposer);

            _onUpgradesClicked.AddTo(Disposer);
            _onUpgradesClicked.BindTo(_shopButton).AddTo(Disposer);

            _currencyText.text = string.Empty;

            UpdateTexts();
        }

        private void UpdateTexts()
        {
            _distanceReachedText.text = TextService.DistanceReached();
            _newBestText.text = TextService.NewBest();
        }

        public void SetDistance(float value)
        {
            _distanceText.text = TextService.MetersAmount(value);
        }

        public void SetBestDistance(float value)
        {
            _bestDistanceText.text = TextService.MetersAmount(value);
        }

        public void SetIsNewBestDistance(bool value)
        {
            _newBestText.gameObject.SetActive(value);
        }

        public void ShowCurrencyResults(IDictionary<CurrencyGainType, int> currencyGains, int initialCurrencyAmount)
        {
            var currencyGainsSequence = DOTween.Sequence()
                .Pause();

            foreach (var currencyGainType in currencyGains.Keys)
            {
                var roundEarningsItemView = _roundEarningsItemViewFactory.Create(
                    _viewPrefabConfig.CurrencyGainItemViewPrefab);

                roundEarningsItemView.transform.SetParent(
                    _currencyGainsLayoutParent,
                    false);

                roundEarningsItemView.Initialize();

                // Hide Element, and activate it when it's turn comes
                roundEarningsItemView.SetActive(false);
                currencyGainsSequence.AppendCallback(() =>
                {
                    roundEarningsItemView.SetActive(true);
                });

                // Then comes the Counting tween
                var tween = roundEarningsItemView.SetupValueCountingTween(
                    currencyGainType,
                    currencyGains[currencyGainType],
                    CurrencyCounterSeconds);

                currencyGainsSequence.Append(tween);
            }

            // At the end the total result tween
            _currencyText.text = TextService.CurrencyAmount(initialCurrencyAmount);
            var totalResultTween = CreateTotalResultTween(currencyGains, initialCurrencyAmount);
            currencyGainsSequence.Append(totalResultTween);

            currencyGainsSequence.Play();
        }

        private Tween CreateTotalResultTween(IDictionary<CurrencyGainType, int> currencyGains, int initialCurrencyAmount)
        {
            var totalSum = currencyGains.Values.Sum() + initialCurrencyAmount;

            return DOTween.To(
                x => _currencyText.text = TextService.CurrencyAmount(x),
                initialCurrencyAmount,
                totalSum,
                CurrencyCounterSeconds)
                .AddTo(Disposer, TweenDisposalBehaviour.Rewind);
        }
    }
}