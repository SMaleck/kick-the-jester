using Assets.Source.Features.Statistics;
using System.Linq;
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
                .Where(_ => !_achievementModel.IsUnlocked.Value)
                .Subscribe(_ => OnHasReachedMoonChanged());
        }

        private void OnHasReachedMoonChanged()
        {
            var isUnlocked = _statisticsModel.HasReachedMoon.Value;
            if (isUnlocked)
            {
                UnlockAchievement();
            }
        }
    }
}
