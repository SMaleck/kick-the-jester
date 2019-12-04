using System;

namespace Assets.Source.Features.Achievements.RequirementStrategies
{
    public interface IRequirementStrategy
    {
        IObservable<AchievementId> OnAchievementUnlocked { get; }
    }
}
