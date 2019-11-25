using Assets.Source.Services.Localization;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class PauseView : ClosableView
    {
        [Header("Labels")]
        [SerializeField] private TextMeshProUGUI _titleText;

        [Header("Buttons")]
        [SerializeField] private Button _openSettingsButton;
        [SerializeField] private TextMeshProUGUI _openSettingsButtonText;
        [SerializeField] private Button _retryButton;
        [SerializeField] private TextMeshProUGUI _retryButtonText;
        [SerializeField] private TextMeshProUGUI _continueButtonText;


        private readonly ReactiveCommand _onSettingsClicked = new ReactiveCommand();
        public IObservable<Unit> OnSettingsClicked => _onSettingsClicked;

        private readonly ReactiveCommand _onRetryClicked = new ReactiveCommand();
        public IObservable<Unit> OnRetryClicked => _onRetryClicked;

        public override void Setup()
        {
            base.Setup();

            _onSettingsClicked.AddTo(Disposer);
            _onSettingsClicked.BindTo(_openSettingsButton).AddTo(Disposer);

            _onRetryClicked.AddTo(Disposer);
            _onRetryClicked.BindTo(_retryButton).AddTo(Disposer);

            UpdateTexts();
        }

        private void UpdateTexts()
        {
            _titleText.text = TextService.Pause();
            _openSettingsButtonText.text = TextService.Settings();
            _retryButtonText.text = TextService.Restart();
            _continueButtonText.text = TextService.Continue();
        }
    }
}
