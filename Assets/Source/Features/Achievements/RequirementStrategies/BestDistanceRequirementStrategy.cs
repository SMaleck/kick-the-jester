using Assets.Source.Features.Statistics;
using System.Linq;
using Assets.Source.Util;
using UniRx;
using Zenject;

namespace Assets.Source.Features.Achievements.RequirementStrategies
{
    public class BestDistanceRequirementStrategy : AbstractRequirementStrategy
    {
        public class Factory : PlaceholderFactory<AchievementModel, BestDistanceRequirementStrategy> { }

        private readonly AchievementModel _achievementModel;
        private readonly IStatisticsModel _statisticsModel;

        public BestDistanceRequirementStrategy(
            AchievementModel achievementModel,
            IStatisticsModel statisticsModel)
            : base(achievementModel)
        {
            _achievementModel = achievementModel;
            _statisticsModel = statisticsModel;

            _statisticsModel.BestDistanceUnits
                .Where(_ => !achievementModel.IsUnlocked.Value)
                .Subscribe(OnTotalDistanceChanged);
        }

        private void OnTotalDistanceChanged(float bestDistanceUnits)
        {
            _achievementModel.SetRequirementProgress(bestDistanceUnits);
            var isUnlocked = bestDistanceUnits.ToMeters() >= _achievementModel.Requirement;

            if (isUnlocked)
            {
                UnlockAchievement();
            }
        }
    }
}