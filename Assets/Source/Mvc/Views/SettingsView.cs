using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class SettingsView : ClosableView
    {
        [SerializeField] private Toggle _isMusicMuted;
        [SerializeField] private Slider _musicVolumeSlider;               
        [SerializeField] private Toggle _isEffectsMuted;
        [SerializeField] private Slider _effectsVolumeSlider;
        [SerializeField] private Button _restoreDefaultsButton;

        [HideInInspector]
        public BoolReactiveProperty IsMusicMuted = new BoolReactiveProperty();
        public FloatReactiveProperty MusicVolume = new FloatReactiveProperty();
        public BoolReactiveProperty IsEffectsMuted = new BoolReactiveProperty();
        public FloatReactiveProperty EffectsVolume = new FloatReactiveProperty();

        public ReactiveCommand OnRestoreDefaultsClicked = new ReactiveCommand();

        public override void Setup()
        {
            base.Setup();

            _isMusicMuted.OnValueChangedAsObservable()
                .Subscribe(value => IsMusicMuted.Value = value)
                .AddTo(this);

            //MusicVolume.Subscribe(e => _musicVolumeSlider.value = e)
            //    .AddTo(this);

            //_musicVolumeSlider.OnValueChangedAsObservable()
            //    .Subscribe(e => MusicVolume.Value = e)
            //    .AddTo(this);


            _isEffectsMuted.OnValueChangedAsObservable()
                .Subscribe(value => IsEffectsMuted.Value = value)
                .AddTo(this);

            //EffectsVolume.Subscribe(e => _effectsVolumeSlider.value = e)
            //    .AddTo(this);

            //_effectsVolumeSlider.OnValueChangedAsObservable()
            //    .Subscribe(e => EffectsVolume.Value = e)
            //    .AddTo(this);


            _restoreDefaultsButton.OnClickAsObservable()
                .Subscribe(_ => OnRestoreDefaultsClicked.Execute())
                .AddTo(this);
        }
    }
}
