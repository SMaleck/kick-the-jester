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

        [HideInInspector]
        public BoolReactiveProperty IsMusicMuted = new BoolReactiveProperty();       
        public BoolReactiveProperty IsEffectsMuted = new BoolReactiveProperty();

        public ReactiveCommand OnRestoreDefaultsClicked = new ReactiveCommand();

        private ReactiveCommand _onResetClicked;
        public IObservable<Unit> OnResetClicked => _onResetClicked;

        public override void Setup()
        {
            base.Setup();

            SetSettingsViewState();

            _onResetClicked = new ReactiveCommand().AddTo(Disposer);

            _isMusicMuted.OnValueChangedAsObservable()
                .Subscribe(_ => IsMusicMuted.Value = !_isMusicMuted.isOn)
                .AddTo(this);
            
            _isEffectsMuted.OnValueChangedAsObservable()
                .Subscribe(_ => IsEffectsMuted.Value = !_isEffectsMuted.isOn)
                .AddTo(this);


            _restoreDefaultsButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    OnRestoreDefaultsClicked.Execute();
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
