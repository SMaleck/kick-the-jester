using Assets.Source.Features.Statistics;
using System.Linq;
using Assets.Source.Util;
using UniRx;
using Zenject;

namespace Assets.Source.Features.Achievements.RequirementStrategies
{
    public class TotalDistanceRequirementStrategy : AbstractRequirementStrategy
    {
        public class Factory : PlaceholderFactory<AchievementModel, TotalDistanceRequirementStrategy> { }

        private readonly AchievementModel _achievementModel;
        private readonly IStatisticsModel _statisticsModel;

        public TotalDistanceRequirementStrategy(
            AchievementModel achievementModel,
            IStatisticsModel statisticsModel)
            : base(achievementModel)
        {
            _achievementModel = achievementModel;
            _statisticsModel = statisticsModel;

            _statisticsModel.TotalDistanceUnits
                .Where(_ => !achievementModel.IsUnlocked.Value)
                .Subscribe(OnTotalDistanceChanged);
        }

        private void OnTotalDistanceChanged(float totalDistanceUnits)
        {
            var isUnlocked = totalDistanceUnits.ToMeters() >= _achievementModel.Requirement;

            if (isUnlocked)
            {
                UnlockAchievement();
            }
        }
    }
}
