using Assets.Source.Features.Statistics;
using System.Linq;
using UniRx;
using Zenject;

namespace Assets.Source.Features.Achievements.RequirementStrategies
{
    public class RoundsPlayedRequirementStrategy : AbstractRequirementStrategy
    {
        public class Factory : PlaceholderFactory<AchievementModel, RoundsPlayedRequirementStrategy> { }

        private readonly AchievementModel _achievementModel;
        private readonly IStatisticsModel _statisticsModel;

        public RoundsPlayedRequirementStrategy(
            AchievementModel achievementModel,
            IStatisticsModel statisticsModel)
            : base(achievementModel)
        {
            _achievementModel = achievementModel;
            _statisticsModel = statisticsModel;

            _statisticsModel.TotalRoundsPlayed
                .Where(_ => !achievementModel.IsUnlocked.Value)
                .Subscribe(OnTotalRoundsPlayedChanges);
        }

        private void OnTotalRoundsPlayedChanges(int roundsPlayed)
        {
            var isUnlocked = roundsPlayed >= _achievementModel.Requirement;

            if (isUnlocked)
            {
                UnlockAchievement();
            }
        }
    }
}
