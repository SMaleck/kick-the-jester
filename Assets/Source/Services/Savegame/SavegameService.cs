using Assets.Source.Services.Savegame.StorageModels;
using Assets.Source.Util.Storage;
using UniRx;

namespace Assets.Source.Services.Savegame
{
    public class SavegameService
    {
        private readonly JsonStorage _storage;
        private const string FileName = "ktj_player.sav";

        private SaveGameStorageModel _saveGameStorageModel;
        private CompositeDisposable _disposer;

        public ProfileStorageModel Profile => _saveGameStorageModel.Profile;
        public UpgradesStorageModel Upgrades => _saveGameStorageModel.Upgrades;
        public SettingsStorageModel Settings => _saveGameStorageModel.Settings;


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

            _saveGameStorageModel.Profile
                .OnAnyPropertyChanged
                .Subscribe(_ => Save())
                .AddTo(_disposer);

            _saveGameStorageModel.Upgrades
                .OnAnyPropertyChanged
                .Subscribe(_ => Save())
                .AddTo(_disposer);

            _saveGameStorageModel.Settings
                .OnAnyPropertyChanged
                .Subscribe(_ => Save())
                .AddTo(_disposer);
        }
        

        public void Load()
        {
            _saveGameStorageModel = _storage.Load<SaveGameStorageModel>();
            SetupModelSubscriptions();
        }


        public void Save()
        {
            _storage.Save(_saveGameStorageModel);
        }


        public void Reset()
        {
            _disposer?.Dispose();

            var isFirstStart = _saveGameStorageModel.Profile.IsFirstStart.Value;

            _saveGameStorageModel.Profile = new ProfileStorageModel();
            _saveGameStorageModel.Upgrades = new UpgradesStorageModel();

            Profile.IsFirstStart.Value = isFirstStart;

            Save();
            SetupModelSubscriptions();
        }
    }
}
