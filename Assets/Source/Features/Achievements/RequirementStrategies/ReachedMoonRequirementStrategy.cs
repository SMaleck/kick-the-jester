using Assets.Source.Features.Statistics;
using UniRx;
using Zenject;

namespace Assets.Source.Features.Achievements.RequirementStrategies
{
    public class ReachedMoonRequirementStrategy : AbstractRequirementStrategy
    {
        public class Factory : PlaceholderFactory<AchievementModel, ReachedMoonRequirementStrategy> { }

        private readonly AchievementModel _achievementModel;
        private readonly IStatisticsModel _statisticsModel;

        public ReachedMoonRequirementStrategy(
            AchievementModel achievementModel,
            IStatisticsModel statisticsModel)
            : base(achievementModel)
        {
            _achievementModel = achievementModel;
            _statisticsModel = statisticsModel;

            _statisticsModel.HasReachedMoon
                .Subscribe(_ => OnHasReachedMoonChanged())
                .AddTo(Disposer);
        }

        private void OnHasReachedMoonChanged()
        {
            if (_statisticsModel.HasReachedMoon.Value &&
                !_achievementModel.IsUnlocked.Value)
            {
                UnlockAchievement();
            }
        }
    }
}
