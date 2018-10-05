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

        [HideInInspector]
        public BoolReactiveProperty IsMusicMuted = new BoolReactiveProperty();
        public BoolReactiveProperty IsEffectsMuted = new BoolReactiveProperty();                
        public ReactiveCommand OnRetryClicked = new ReactiveCommand();

        public override void Setup()
        {
            base.Setup();

            _retryButton.OnClickAsObservable()
                .Subscribe(_ => OnRetryClicked.Execute())
                .AddTo(this);

            _isMusicMuted.OnValueChangedAsObservable()
                .Subscribe(_ => IsMusicMuted.Value = _isMusicMuted.isOn)
                .AddTo(this);

            _isEffectsMuted.OnValueChangedAsObservable()
                .Subscribe(_ => IsEffectsMuted.Value = _isEffectsMuted.isOn)
                .AddTo(this);
        }
    }
}
