using Assets.Source.Features.Statistics;
using System.Linq;
using UniRx;
using Zenject;

namespace Assets.Source.Features.Achievements.RequirementStrategies
{
    public class RoundsPlayedRequirementStrategy : AbstractRequirementStrategy
    {
        public class Factory : PlaceholderFactory<AchievementModel, RoundsPlayedRequirementStrategy> { }

        private readonly IStatisticsModel _statisticsModel;

        public RoundsPlayedRequirementStrategy(
            AchievementModel achievementModel,
            IStatisticsModel statisticsModel)
            : base(achievementModel)
        {
            _statisticsModel = statisticsModel;

            _statisticsModel.TotalRoundsPlayed
                .Where(_ => !achievementModel.IsUnlocked.Value)
                .Subscribe(OnTotalRoundsPlayedChanges)
                .AddTo(Disposer);
        }

        private void OnTotalRoundsPlayedChanges(int roundsPlayed)
        {
            UpdateProgress(roundsPlayed);
        }
    }
}
