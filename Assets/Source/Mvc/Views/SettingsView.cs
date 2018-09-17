using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class SettingsView : ClosableView
    {        
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _effectsVolumeSlider;
        [SerializeField] private Button _restoreDefaultsButton;

        [HideInInspector]
        public FloatReactiveProperty MusicVolume = new FloatReactiveProperty();
        public FloatReactiveProperty EffectsVolume = new FloatReactiveProperty();

        public Action OnRestoreDefaultsClicked = () => {};


        public override void Setup()
        {
            base.Setup();
            // TODO Fix
            //MusicVolume.Subscribe(e => _musicVolumeSlider.value = e)
            //    .AddTo(this);

            //_musicVolumeSlider.OnValueChangedAsObservable()
            //    .Subscribe(e => MusicVolume.Value = e)
            //    .AddTo(this);


            //EffectsVolume.Subscribe(e => _effectsVolumeSlider.value = e)
            //    .AddTo(this);

            //_effectsVolumeSlider.OnValueChangedAsObservable()
            //    .Subscribe(e => EffectsVolume.Value = e)
            //    .AddTo(this);


            //_restoreDefaultsButton.OnClickAsObservable()
            //    .Subscribe(_ => OnRestoreDefaultsClicked())
            //    .AddTo(this);
        }
    }
}
