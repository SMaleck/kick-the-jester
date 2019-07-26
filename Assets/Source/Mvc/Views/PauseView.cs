using Assets.Source.Services;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    // ToDo Integrate with SettingsView    
    public class PauseView : ClosableView
    {
        [Header("Labels")]
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _soundSettingsTitleText;

        [Header("Sound Settings")]
        [SerializeField] private Toggle _isMusicMutedToggle;
        [SerializeField] private TextMeshProUGUI _isMusicMutedText;

        [SerializeField] private Toggle _isEffectsMutedToggle;
        [SerializeField] private TextMeshProUGUI _isEffectsMutedText;

        [Header("Buttons")]
        [SerializeField] private Button _retryButton;

        private readonly ReactiveProperty<bool> _isMusicMutedProp = new ReactiveProperty<bool>();
        public IReadOnlyReactiveProperty<bool> IsMusicMutedProp => _isMusicMutedProp;

        private readonly ReactiveProperty<bool> _isEffectsMutedProp = new ReactiveProperty<bool>();
        public IReadOnlyReactiveProperty<bool> IsEffectsMutedProp => _isEffectsMutedProp;

        private readonly ReactiveCommand _onRetryClicked = new ReactiveCommand();
        public IObservable<Unit> OnRetryClicked => _onRetryClicked;

        public override void Setup()
        {
            base.Setup();

            _isMusicMutedProp.AddTo(Disposer);
            _isEffectsMutedProp.AddTo(Disposer);

            SetSettingsViewState();

            _onRetryClicked.AddTo(Disposer);
            _onRetryClicked.BindTo(_retryButton).AddTo(Disposer);

            _isMusicMutedToggle.OnValueChangedAsObservable()
                .Subscribe(_ => _isMusicMutedProp.Value = !_isMusicMutedToggle.isOn)
                .AddTo(Disposer);

            _isEffectsMutedToggle.OnValueChangedAsObservable()
                .Subscribe(_ => _isEffectsMutedProp.Value = !_isEffectsMutedToggle.isOn)
                .AddTo(Disposer);

            UpdateTexts();
        }

        private void UpdateTexts()
        {
            _titleText.text = TextService.Pause();
            _soundSettingsTitleText.text = TextService.SoundSettings();
            _isMusicMutedText.text = TextService.Music();
            _isEffectsMutedText.text = TextService.SoundEffects();
        }

        public void SetIsMusicMuted(bool value)
        {
            _isMusicMutedProp.Value = value;
        }

        public void SetIsEffectsMuted(bool value)
        {
            _isEffectsMutedProp.Value = value;
        }

        private void SetSettingsViewState()
        {
            _isMusicMutedToggle.isOn = !_isMusicMutedProp.Value;
            _isEffectsMutedToggle.isOn = !_isEffectsMutedProp.Value;
        }
    }
}
