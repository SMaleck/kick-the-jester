using Assets.Source.Services;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class SettingsView : ClosableView
    {
        [Header("Labels")]
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _soundSettingsTitleText;

        [Header("Sound Settings")]
        [SerializeField] private Toggle _isMusicMuted;
        [SerializeField] private TextMeshProUGUI _isMusicMutedText;

        [SerializeField] private Toggle _isEffectsMuted;
        [SerializeField] private TextMeshProUGUI _isEffectsMutedText;

        [Header("Restore Defaults Button")]
        [SerializeField] private Button _restoreDefaultsButton;
        [SerializeField] private TextMeshProUGUI _restoreDefaultsButtonText;

        [Header("Reset Profile Button")]
        [SerializeField] private Button _resetProfileButton;
        [SerializeField] private TextMeshProUGUI _resetProfileButtonText;

        private readonly ReactiveProperty<bool> _isMusicMutedProp = new ReactiveProperty<bool>();
        public IReadOnlyReactiveProperty<bool> IsMusicMutedProp => _isMusicMutedProp;

        private readonly ReactiveProperty<bool> _isEffectsMutedProp = new ReactiveProperty<bool>();
        public IReadOnlyReactiveProperty<bool> IsEffectsMutedProp => _isEffectsMutedProp;

        private readonly ReactiveCommand _onRestoreDefaultsClicked = new ReactiveCommand();
        public IObservable<Unit> OnRestoreDefaultsClicked => _onRestoreDefaultsClicked;

        private readonly ReactiveCommand _onResetProfileClicked = new ReactiveCommand();
        public IObservable<Unit> OnResetProfileClicked => _onResetProfileClicked;

        public override void Setup()
        {
            base.Setup();

            _isMusicMutedProp.AddTo(Disposer);
            _isEffectsMutedProp.AddTo(Disposer);
            _onRestoreDefaultsClicked.AddTo(Disposer);
            _onResetProfileClicked.AddTo(Disposer);

            SetSettingsViewState();

            _isMusicMuted.OnValueChangedAsObservable()
                .Subscribe(_ => _isMusicMutedProp.Value = !_isMusicMuted.isOn)
                .AddTo(Disposer);

            _isEffectsMuted.OnValueChangedAsObservable()
                .Subscribe(_ => _isEffectsMutedProp.Value = !_isEffectsMuted.isOn)
                .AddTo(Disposer);


            _restoreDefaultsButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _onRestoreDefaultsClicked.Execute();
                    SetSettingsViewState();
                })
                .AddTo(this);

            _onResetProfileClicked.BindTo(_resetProfileButton).AddTo(Disposer);

            UpdateTexts();
        }

        private void UpdateTexts()
        {
            _titleText.text = TextService.Settings();
            _soundSettingsTitleText.text = TextService.SoundSettings();
            _isMusicMutedText.text = TextService.Music();
            _isEffectsMutedText.text = TextService.SoundEffects();
            _restoreDefaultsButtonText.text = TextService.RestoreDefaults();
            _resetProfileButtonText.text = TextService.ResetProfile();
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
            _isMusicMuted.isOn = !_isMusicMutedProp.Value;
            _isEffectsMuted.isOn = !_isEffectsMutedProp.Value;
        }
    }
}
