using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Assets.Source.Services.Savegame
{
    public class SavegameService
    {        
        private SaveGameModel _saveGameModel;
        private CompositeDisposable _disposer;

        public FloatReactiveProperty MusicVolume;
        public FloatReactiveProperty EffectVolume;


        public SavegameService()
        {
            Load();
            SetupModelSubscriptions();
        }


        private void SetupModelSubscriptions()
        {
            _disposer?.Dispose();
            _disposer = new CompositeDisposable();

            MusicVolume = new FloatReactiveProperty(_saveGameModel.MusicVolume);
            MusicVolume.Subscribe(e => _saveGameModel.MusicVolume = e);

            EffectVolume = new FloatReactiveProperty(_saveGameModel.EffectsVolume);
            EffectVolume.Subscribe(e => SetValue(() => { _saveGameModel.EffectsVolume = e; } ));
        }


        private void SetValue(Action setter)
        {
            setter();
            Save();
        }


        public void Load()
        {
            _saveGameModel = new SaveGameModel();
        }


        public void Save()
        {
            // TODO persist savegame to JSON
        }
    }
}
