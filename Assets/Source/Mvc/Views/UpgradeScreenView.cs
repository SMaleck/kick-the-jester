using Assets.Source.Services;
using System;
using Assets.Source.Services.Localization;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class UpgradeScreenView : ClosableView
    {
        [SerializeField] private Transform _upgradeItemsLayoutParent;
        [SerializeField] private TextMeshProUGUI _currencyAmountText;

        [Header("Play Again Button")]
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private TextMeshProUGUI _playAgainButtonText;

        [Header("Reset Profile Button")]
        [SerializeField] private Button _resetProfileButton;
        [SerializeField] private TextMeshProUGUI _resetProfileButtonText;

        public Transform UpgradeItemsLayoutParent => _upgradeItemsLayoutParent;

        private readonly ReactiveCommand _onPlayAgainClicked = new ReactiveCommand();
        public IObservable<Unit> OnPlayAgainClicked => _onPlayAgainClicked;

        private readonly ReactiveCommand _onResetClicked = new ReactiveCommand();
        public IObservable<Unit> OnResetClicked => _onResetClicked;

        public override void Setup()
        {
            base.Setup();

            _onPlayAgainClicked.AddTo(Disposer);
            _onPlayAgainClicked.BindTo(_playAgainButton).AddTo(Disposer);

            _onResetClicked.AddTo(Disposer);
            _onResetClicked.BindTo(_resetProfileButton).AddTo(Disposer);

            _playAgainButtonText.text = TextService.PlayAgainExclamation();
            _resetProfileButtonText.text = TextService.ResetProfile();
        }

        public void SetCurrencyAmount(int amount)
        {
            _currencyAmountText.text = TextService.CurrencyAmount(amount);
        }
    }
}
