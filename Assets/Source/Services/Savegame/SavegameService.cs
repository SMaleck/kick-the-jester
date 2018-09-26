using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Source.Util.Storage;
using UniRx;

namespace Assets.Source.Services.Savegame
{
    public class SavegameService
    {
        private readonly JsonStorage _storage;
        private const string FileName = "ktj_player.sav";

        private SaveGameStorageModel _saveGameModel;
        private CompositeDisposable _disposer;

        public FloatReactiveProperty MusicVolume;
        public FloatReactiveProperty EffectVolume;


        public SavegameService()
        {
            _storage = new JsonStorage(FileName);
            Load();

            Observable.OnceApplicationQuit()
                .Subscribe(_ => Save())
                .AddTo(_disposer);
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
            _saveGameModel = _storage.Load<SaveGameStorageModel>();
            SetupModelSubscriptions();
        }


        public void Save()
        {
            _storage.Save(_saveGameModel);
        }
    }
}
