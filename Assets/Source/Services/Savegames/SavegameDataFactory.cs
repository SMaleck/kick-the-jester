using System.Collections.Generic;
using Assets.Source.Features.Upgrades.Data;
using Assets.Source.Services.Savegames.Models;
using Assets.Source.Util;
using System.Linq;
using Assets.Source.Features.Achievements;

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
                StatisticsSavegameData = CreateStatisticsSavegameData(),
                AchievementsSavegameData = CreateAchievementsSavegameData()
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
            var upgradeSavegames = EnumHelper<UpgradePathType>
                .Iterator
                .Select(CreateUpgradeSavegameData)
                .ToList();

            return new UpgradesSavegameData()
            {
                UpgradeSavegames = upgradeSavegames
            };
        }

        private static UpgradeSavegameData CreateUpgradeSavegameData(UpgradePathType upgradePathType)
        {
            return new UpgradeSavegameData()
            {
                UpgradePathType = (int)upgradePathType,
                Level = 0
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
                TotalCurrencyCollected = 0,
                TotalRoundsPlayed = 0,
                HasReachedMoon = false
            };
        }

        private static AchievementsSavegameData CreateAchievementsSavegameData()
        {
            return new AchievementsSavegameData()
            {
                AchievementSavegames = CreateAchievementSavegames()
            };
        }

        private static List<AchievementSavegameData> CreateAchievementSavegames()
        {
            return EnumHelper<AchievementId>.Iterator
                .Select(id => new AchievementSavegameData
                {
                    Id = (int)id,
                    IsUnlocked = false
                })
                .ToList();
        }
    }
}
