using Assets.Source.Features.Statistics;
using Assets.Source.Util;
using UniRx;
using Zenject;

namespace Assets.Source.Features.Achievements.RequirementStrategies
{
    public class TotalDistanceRequirementStrategy : AbstractRequirementStrategy
    {
        public class Factory : PlaceholderFactory<AchievementModel, TotalDistanceRequirementStrategy> { }

        private readonly IStatisticsModel _statisticsModel;

        public TotalDistanceRequirementStrategy(
            AchievementModel achievementModel,
            IStatisticsModel statisticsModel)
            : base(achievementModel)
        {
            _statisticsModel = statisticsModel;

            _statisticsModel.TotalDistanceUnits
                .Subscribe(OnTotalDistanceChanged)
                .AddTo(Disposer);
        }

        private void OnTotalDistanceChanged(float totalDistanceUnits)
        {
            UpdateProgress(totalDistanceUnits.ToMeters());
        }
    }
}
