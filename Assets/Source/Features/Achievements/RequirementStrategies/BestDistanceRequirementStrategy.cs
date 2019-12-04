using Assets.Source.Features.Statistics;
using System.Linq;
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

            _statisticsModel.BestDistance
                .Where(_ => !achievementModel.IsUnlocked.Value)
                .Subscribe(OnTotalDistanceChanged);
        }

        private void OnTotalDistanceChanged(float bestDistance)
        {
            var isUnlocked = bestDistance >= _achievementModel.Requirement;

            if (isUnlocked)
            {
                UnlockAchievement();
            }
        }
    }
}