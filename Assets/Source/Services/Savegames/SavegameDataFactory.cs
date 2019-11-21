using Assets.Source.Services.Savegames.Models;

namespace Assets.Source.Services.Savegames
{
    public static class SavegameDataFactory
    {
        public static SavegameData CreateSavegameData()
        {
            return new SavegameData()
            {
                ProfileSavegameData = CreateProfileSavegameData(),
                UpgradesSavegameData = CreateUpgradesSavegameData(),
                SettingsSavegameData = CreateSettingsSavegameData(),
                StatisticsSavegameData = CreateStatisticsSavegameData()
            };
        }

        private static ProfileSavegameData CreateProfileSavegameData()
        {
            return new ProfileSavegameData()
            {
                IsFirstStart = true,
                Currency = 0
            };
        }

        private static UpgradesSavegameData CreateUpgradesSavegameData()
        {
            return new UpgradesSavegameData()
            {
                MaxVelocityLevel = 0,
                KickForceLevel = 0,
                ShootForceLevel = 0,
                ShootCountLevel = 0
            };
        }

        private static SettingsSavegameData CreateSettingsSavegameData()
        {
            return new SettingsSavegameData()
            {
                IsMusicMuted = false,
                IsSoundMuted = false
            };
        }

        private static StatisticsSavegameData CreateStatisticsSavegameData()
        {
            return new StatisticsSavegameData()
            {
                BestDistance = 0,
                TotalDistance = 0,
                BestHeight = 0,
                TotalHeight = 0,
                TotalCurrencyCollected = 0,
                TotalRoundsPlayed = 0,
                HasReachedMoon = false
            };
        }
    }
}
