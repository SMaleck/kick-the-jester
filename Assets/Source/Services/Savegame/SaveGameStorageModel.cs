using Assets.Source.Services.Savegame.StorageModels;

namespace Assets.Source.Services.Savegame
{
    public class SaveGameStorageModel
    {
        public ProfileStorageModel Profile = new ProfileStorageModel();
        public UpgradesStorageModel Upgrades = new UpgradesStorageModel();
        public SettingsStorageModel Settings = new SettingsStorageModel(); 
    }
}
