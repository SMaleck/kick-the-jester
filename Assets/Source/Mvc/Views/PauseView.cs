using Assets.Source.Services.Localization;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class PauseView : AbstractView
    {
        [Header("Labels")]
        [SerializeField] private TextMeshProUGUI _titleText;

        [Header("Buttons")]
        [SerializeField] private TextMeshProUGUI _continueButtonText;
        [SerializeField] private Button _retryButton;
        [SerializeField] private TextMeshProUGUI _retryButtonText;
        [SerializeField] private Button _openSettingsButton;
        [SerializeField] private TextMeshProUGUI _openSettingsButtonText;
        [SerializeField] private Button _achievementsButton;
        [SerializeField] private TextMeshProUGUI _achievementsButtonText;


        private readonly ReactiveCommand _onSettingsClicked = new ReactiveCommand();
        public IObservable<Unit> OnSettingsClicked => _onSettingsClicked;

        private readonly ReactiveCommand _onRetryClicked = new ReactiveCommand();
        public IObservable<Unit> OnRetryClicked => _onRetryClicked;

        private readonly ReactiveCommand _onAchievementsClicked = new ReactiveCommand();
        public IObservable<Unit> OnAchievementsClicked => _onAchievementsClicked;

        public override void Setup()
        {
            _onSettingsClicked.AddTo(Disposer);
            _onSettingsClicked.BindTo(_openSettingsButton).AddTo(Disposer);

            _onRetryClicked.AddTo(Disposer);
            _onRetryClicked.BindTo(_retryButton).AddTo(Disposer);

            _onAchievementsClicked.AddTo(Disposer);
            _onAchievementsClicked.BindTo(_achievementsButton).AddTo(Disposer);

            UpdateTexts();
        }

        private void UpdateTexts()
        {
            _titleText.text = TextService.Pause();
            _continueButtonText.text = TextService.Continue();
            _retryButtonText.text = TextService.Restart();
            _openSettingsButtonText.text = TextService.Settings();
            _achievementsButtonText.text = TextService.Achievements();
        }
    }
}
