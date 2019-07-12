using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    // ToDo Integrate with SettingsView    
    public class PauseView : ClosableView
    {
        [SerializeField] private Toggle _isMusicMuted;
        [SerializeField] private Toggle _isEffectsMuted;                
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

            _isMusicMuted.OnValueChangedAsObservable()
                .Subscribe(_ => _isMusicMutedProp.Value = !_isMusicMuted.isOn)
                .AddTo(Disposer);

            _isEffectsMuted.OnValueChangedAsObservable()
                .Subscribe(_ => _isEffectsMutedProp.Value = !_isEffectsMuted.isOn)
                .AddTo(Disposer);
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
