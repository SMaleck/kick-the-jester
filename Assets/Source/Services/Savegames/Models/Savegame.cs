namespace Assets.Source.Services.Savegames.Models
{
    public class SavegameData : AbstractSavegameData
    {
        public ProfileSavegameData ProfileSavegameData;
        public UpgradesSavegameData UpgradesSavegameData;
        public SettingsSavegameData SettingsSavegameData;
        public StatisticsSavegameData StatisticsSavegameData;
        public AchievementsSavegameData AchievementsSavegameData;
    }

    public class Savegame : AbstractSavegame
    {
        public readonly ProfileSavegame ProfileSavegame;
        public readonly UpgradesSavegame UpgradesSavegame;
        public readonly SettingsSavegame SettingsSavegame;
        public readonly StatisticsSavegame StatisticsSavegame;
        public readonly AchievementsSavegame AchievementsSavegame;

        public Savegame(SavegameData savegameData)
        {
            ProfileSavegame = new ProfileSavegame(savegameData.ProfileSavegameData);
            UpgradesSavegame = new UpgradesSavegame(savegameData.UpgradesSavegameData);
            SettingsSavegame = new SettingsSavegame(savegameData.SettingsSavegameData);
            StatisticsSavegame = new StatisticsSavegame(savegameData.StatisticsSavegameData);
            AchievementsSavegame = new AchievementsSavegame(savegameData.AchievementsSavegameData);
        }
    }
}
