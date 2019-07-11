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
        [SerializeField] private Button _resetProfileButton;

        [HideInInspector]
        public BoolReactiveProperty IsMusicMuted = new BoolReactiveProperty();       
        public BoolReactiveProperty IsEffectsMuted = new BoolReactiveProperty();

        private readonly ReactiveCommand _onRestoreDefaultsClicked = new ReactiveCommand();
        public IObservable<Unit> OnRestoreDefaultsClicked => _onRestoreDefaultsClicked;

        private readonly ReactiveCommand _onResetProfileClicked = new ReactiveCommand();
        public IObservable<Unit> OnResetProfileClicked => _onResetProfileClicked;

        public override void Setup()
        {
            base.Setup();

            SetSettingsViewState();

            _isMusicMuted.OnValueChangedAsObservable()
                .Subscribe(_ => IsMusicMuted.Value = !_isMusicMuted.isOn)
                .AddTo(this);
            
            _isEffectsMuted.OnValueChangedAsObservable()
                .Subscribe(_ => IsEffectsMuted.Value = !_isEffectsMuted.isOn)
                .AddTo(this);


            _restoreDefaultsButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _onRestoreDefaultsClicked.Execute();
                    SetSettingsViewState();
                })
                .AddTo(this);

            _resetProfileButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _onResetProfileClicked.Execute();
                    SetSettingsViewState();
                })
                .AddTo(this);
        }

        private void SetSettingsViewState()
        {
            _isMusicMuted.isOn = !IsMusicMuted.Value;
            _isEffectsMuted.isOn = !IsEffectsMuted.Value;
        }
    }
}
