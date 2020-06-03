using Assets.Source.Features.Statistics;
using Assets.Source.Util;
using System.Linq;
using UniRx;
using Zenject;

namespace Assets.Source.Features.Achievements.RequirementStrategies
{
    public class BestHeightRequirementStrategy : AbstractRequirementStrategy
    {
        public class Factory : PlaceholderFactory<AchievementModel, BestHeightRequirementStrategy> { }

        private readonly AchievementModel _achievementModel;
        private readonly IStatisticsModel _statisticsModel;

        public BestHeightRequirementStrategy(
            AchievementModel achievementModel,
            IStatisticsModel statisticsModel)
            : base(achievementModel)
        {
            _achievementModel = achievementModel;
            _statisticsModel = statisticsModel;

            _statisticsModel.BestHeightUnits
                .Where(_ => !achievementModel.IsUnlocked.Value)
                .Subscribe(BestHeightUnitsChanged);
        }

        private void BestHeightUnitsChanged(float bestHeight)
        {
            _achievementModel.SetRequirementProgress(bestHeight);
            var isUnlocked = bestHeight.ToMeters() >= _achievementModel.Requirement;

            if (isUnlocked)
            {
                UnlockAchievement();
            }
        }
    }
}
