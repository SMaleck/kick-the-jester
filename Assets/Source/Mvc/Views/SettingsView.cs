using Assets.Source.Services;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class SettingsView : ClosableView
    {
        [SerializeField] private Toggle _isMusicMuted;
        [SerializeField] private Toggle _isEffectsMuted;
        [SerializeField] private Button _restoreDefaultsButton;

        [Header("Reset Profile Button")]
        [SerializeField] private Button _resetProfileButton;
        [SerializeField] private Text _resetProfileButtonText;

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

            _isMusicMutedProp.AddTo(this);
            _isEffectsMutedProp.AddTo(this);
            _onRestoreDefaultsClicked.AddTo(this);
            _onResetProfileClicked.AddTo(this);

            SetSettingsViewState();

            _isMusicMuted.OnValueChangedAsObservable()
                .Subscribe(_ => _isMusicMutedProp.Value = !_isMusicMuted.isOn)
                .AddTo(this);

            _isEffectsMuted.OnValueChangedAsObservable()
                .Subscribe(_ => _isEffectsMutedProp.Value = !_isEffectsMuted.isOn)
                .AddTo(this);


            _restoreDefaultsButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _onRestoreDefaultsClicked.Execute();
                    SetSettingsViewState();
                })
                .AddTo(this);

            _onResetProfileClicked.BindTo(_resetProfileButton);
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
