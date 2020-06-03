using Assets.Source.Features.Statistics;
using Assets.Source.Util;
using UniRx;
using Zenject;

namespace Assets.Source.Features.Achievements.RequirementStrategies
{
    public class BestHeightRequirementStrategy : AbstractRequirementStrategy
    {
        public class Factory : PlaceholderFactory<AchievementModel, BestHeightRequirementStrategy> { }

        private readonly IStatisticsModel _statisticsModel;

        public BestHeightRequirementStrategy(
            AchievementModel achievementModel,
            IStatisticsModel statisticsModel)
            : base(achievementModel)
        {
            _statisticsModel = statisticsModel;

            _statisticsModel.BestHeightUnits
                .Subscribe(BestHeightUnitsChanged)
                .AddTo(Disposer);
        }

        private void BestHeightUnitsChanged(float bestHeight)
        {
            UpdateProgress(bestHeight.ToMeters());
        }
    }
}
