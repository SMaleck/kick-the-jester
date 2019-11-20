namespace Assets.Source.Services.Savegames.Models
{
    public class SavegameData : AbstractSavegameData
    {
        public ProfileSavegameData ProfileSavegameData;
        public UpgradesSavegameData UpgradesSavegameData;
        public SettingsSavegameData SettingsSavegameData;
    }

    public class Savegame : AbstractSavegame
    {
        public ProfileSavegame ProfileSavegame;
        public UpgradesSavegame UpgradesSavegame;
        public SettingsSavegame SettingsSavegame;

        public Savegame(SavegameData savegameData)
        {
            ProfileSavegame = new ProfileSavegame(savegameData.ProfileSavegameData);
            UpgradesSavegame = new UpgradesSavegame(savegameData.UpgradesSavegameData);
            SettingsSavegame = new SettingsSavegame(savegameData.SettingsSavegameData);
        }
    }
}
