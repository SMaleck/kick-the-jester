using Assets.Source.Features.Statistics;
using Assets.Source.Util;
using UniRx;
using Zenject;

namespace Assets.Source.Features.Achievements.RequirementStrategies
{
    public class BestDistanceRequirementStrategy : AbstractRequirementStrategy
    {
        public class Factory : PlaceholderFactory<AchievementModel, BestDistanceRequirementStrategy> { }

        private readonly IStatisticsModel _statisticsModel;

        public BestDistanceRequirementStrategy(
            AchievementModel achievementModel,
            IStatisticsModel statisticsModel)
            : base(achievementModel)
        {
            _statisticsModel = statisticsModel;

            _statisticsModel.BestDistanceUnits
                .Subscribe(OnTotalDistanceChanged)
                .AddTo(Disposer);
        }

        private void OnTotalDistanceChanged(float bestDistanceUnits)
        {
            UpdateProgress(bestDistanceUnits.ToMeters());
        }
    }
}