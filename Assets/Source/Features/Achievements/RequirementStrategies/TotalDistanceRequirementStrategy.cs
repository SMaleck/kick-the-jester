using Assets.Source.Features.Statistics;
using System.Linq;
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

            _statisticsModel.TotalDistance
                .Where(_ => !achievementModel.IsUnlocked.Value)
                .Subscribe(OnTotalDistanceChanged);
        }

        private void OnTotalDistanceChanged(float totalDistance)
        {
            var isUnlocked = totalDistance >= _achievementModel.Requirement;

            if (isUnlocked)
            {
                UnlockAchievement();
            }
        }
    }
}
