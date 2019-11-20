using Assets.Source.Services.Savegames.Models;
using Assets.Source.Util.Storage;
using System;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Services.Savegames
{
    public class SavegameService : AbstractDisposable
    {
        private const double RequestSaveTimeoutSeconds = 1d;
        private const string FileName = "ktj_player.sav";

        private readonly JsonStorage _storage;

        private SavegameData _savegameData;
        private Savegame _savegame;
        public Savegame Savegame => GetSavegame();

        private readonly SerialDisposable _savegameDisposer;

        private readonly SerialDisposable _saveDisposer;
        private CompositeDisposable _disposer;
        private readonly TimeSpan _requestSaveTimeout;

        public SavegameService()
        {
            _savegameDisposer = new SerialDisposable().AddTo(Disposer);
            _saveDisposer = new SerialDisposable().AddTo(Disposer);
            _requestSaveTimeout = TimeSpan.FromSeconds(RequestSaveTimeoutSeconds);

            _storage = new JsonStorage(FileName);
            Load();

            Observable.OnceApplicationQuit()
                .Subscribe(_ => Save())
                .AddTo(Disposer);
        }

        private Savegame GetSavegame()
        {
            if (_savegame == null)
            {
                Load();
            }

            return _savegame;
        }

        private void SetupModelSubscriptions()
        {
            _disposer?.Dispose();
            _disposer = new CompositeDisposable();

            //_saveGameStorageModel.Profile
            //    .OnAnyPropertyChanged
            //    .Subscribe(_ => RequestSave())
            //    .AddTo(_disposer);

            //_saveGameStorageModel.Upgrades
            //    .OnAnyPropertyChanged
            //    .Subscribe(_ => RequestSave())
            //    .AddTo(_disposer);

            //_saveGameStorageModel.Settings
            //    .OnAnyPropertyChanged
            //    .Subscribe(_ => RequestSave())
            //    .AddTo(_disposer);
        }

        private void RequestSave()
        {
            App.Logger.Log($"Savegame dirty, saving in {_requestSaveTimeout.TotalSeconds}s");

            _saveDisposer.Disposable = Observable.Timer(_requestSaveTimeout)
                .Subscribe(_ => Save());
        }

        public void Load()
        {
            _savegameData = _storage.Load<SavegameData>();
            _savegameData = _savegameData ?? SavegameDataFactory.CreateSavegameData();

            SetupSavegame();
            SetupModelSubscriptions();
        }

        public void Save()
        {
            _saveDisposer.Disposable?.Dispose();
            _storage.Save(_savegameData);
        }

        public void Reset()
        {
            _disposer?.Dispose();

            var isFirstStart = _savegame.ProfileSavegame.IsFirstStart.Value;

            _savegameData = SavegameDataFactory.CreateSavegameData();
            SetupSavegame();

            _savegame.ProfileSavegame.IsFirstStart.Value = isFirstStart;

            Save();
            SetupModelSubscriptions();
        }

        private void SetupSavegame()
        {
            _savegameDisposer.Disposable?.Dispose();

            _savegame = new Savegame(_savegameData);
            _savegameDisposer.Disposable = _savegame;
        }
    }
}
