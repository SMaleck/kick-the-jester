using Assets.Source.Services.Savegames.Models;
using Assets.Source.Util;
using Assets.Source.Util.DataStorageStrategies;
using System;
using UniRx;

namespace Assets.Source.Services.Savegames
{
    public class SavegameService : AbstractDisposable, ISavegameService, ISavegamePersistenceService
    {
        private const double RequestSaveTimeoutSeconds = 1d;
        private const string FileName = "ktj_player.sav";

        private readonly IDataStorageStrategy _dataStorageStrategy;
        private readonly SerialDisposable _savegameDisposer;
        private readonly SerialDisposable _saveDisposer;
        private readonly TimeSpan _requestSaveTimeout;

        private SavegameData _savegameData;
        private Savegame _savegame;

        Savegame ISavegameService.Savegame => GetSavegame();

        private ISavegamePersistenceService _savegamePersistenceService => this;

        public SavegameService(IDataStorageStrategy dataStorageStrategy)
        {
            _dataStorageStrategy = dataStorageStrategy;

            _savegameDisposer = new SerialDisposable().AddTo(Disposer);
            _saveDisposer = new SerialDisposable().AddTo(Disposer);
            _requestSaveTimeout = TimeSpan.FromSeconds(RequestSaveTimeoutSeconds);
        }

        private Savegame GetSavegame()
        {
            if (_savegame == null)
            {
                _savegamePersistenceService.Load();
            }

            return _savegame;
        }

        void ISavegamePersistenceService.Load()
        {
            _savegameData = _dataStorageStrategy.Load<SavegameData>(FileName);
            _savegameData = _savegameData ?? SavegameDataFactory.CreateSavegameData();

            SetupSavegame();
        }

        void ISavegamePersistenceService.EnqueueSaveRequest()
        {
            App.Logger.Log($"Savegame dirty, saving in {_requestSaveTimeout.TotalSeconds}s");

            _saveDisposer.Disposable = Observable.Timer(_requestSaveTimeout)
                .Subscribe(_ => _savegamePersistenceService.Save());
        }

        void ISavegamePersistenceService.Save()
        {
            _saveDisposer.Disposable?.Dispose();
            _dataStorageStrategy.Save(FileName, _savegameData);
        }

        void ISavegameService.Reset()
        {
            var isFirstStart = _savegame.ProfileSavegame.IsFirstStart.Value;

            _savegameData = SavegameDataFactory.CreateSavegameData();
            SetupSavegame();

            _savegame.ProfileSavegame.IsFirstStart.Value = isFirstStart;

            _savegamePersistenceService.Save();
        }

        private void SetupSavegame()
        {
            _savegameDisposer.Disposable?.Dispose();

            _savegame = new Savegame(_savegameData);
            _savegameDisposer.Disposable = _savegame;
        }
    }
}
