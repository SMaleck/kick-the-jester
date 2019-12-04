using Assets.Source.Features.Achievements.RequirementStrategies;
using Assets.Source.Util;
using System.Collections.Generic;
using UniRx;
using Zenject;

namespace Assets.Source.Features.Achievements
{
    public class AchievementUnlockController : AbstractDisposable
    {
        public class Factory : PlaceholderFactory<List<IRequirementStrategy>, AchievementUnlockController> { }

        public AchievementUnlockController(List<IRequirementStrategy> requirementStrategies)
        {
            requirementStrategies.ForEach(SubscribeToUnlock);
        }

        private void SubscribeToUnlock(IRequirementStrategy requirementStrategy)
        {
            requirementStrategy.OnAchievementUnlocked
                .Subscribe(OnAchievementUnlocked)
                .AddTo(Disposer);
        }

        private void OnAchievementUnlocked(AchievementId achievementId)
        {
            // ToDo Report unlock to external API
            App.Logger.Warn($"ACHIEVEMENT UNLOCKED: {achievementId}");
        }
    }
}
