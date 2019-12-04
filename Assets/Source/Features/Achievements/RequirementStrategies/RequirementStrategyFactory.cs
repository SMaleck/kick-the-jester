using System;
using Zenject;

namespace Assets.Source.Features.Achievements.RequirementStrategies
{
    public class RequirementStrategyFactory
    {
        [Inject] private readonly ReachedMoonRequirementStrategy.Factory _reachedMoonRequirementStrategyFactory;
        [Inject] private readonly TotalDistanceRequirementStrategy.Factory _totalDistanceRequirementStrategyFactory;
        [Inject] private readonly BestDistanceRequirementStrategy.Factory _bestDistanceRequirementStrategyFactory;

        public IRequirementStrategy CreateRequirementStrategy(AchievementModel achievementModel)
        {
            switch (achievementModel.RequirementType)
            {
                case AchievementRequirementType.ReachMoon:
                    return _reachedMoonRequirementStrategyFactory.Create(achievementModel);

                case AchievementRequirementType.TotalDistance:
                    return _totalDistanceRequirementStrategyFactory.Create(achievementModel);

                case AchievementRequirementType.BestDistance:
                    return _bestDistanceRequirementStrategyFactory.Create(achievementModel);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
