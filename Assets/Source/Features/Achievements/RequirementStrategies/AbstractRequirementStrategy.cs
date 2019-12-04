using Assets.Source.Util;
using System;
using UniRx;

namespace Assets.Source.Features.Achievements.RequirementStrategies
{
    public abstract class AbstractRequirementStrategy : AbstractDisposable, IRequirementStrategy
    {
        private readonly AchievementModel _achievementModel;

        private readonly Subject<AchievementId> _onAchievementUnlocked;
        public IObservable<AchievementId> OnAchievementUnlocked => _onAchievementUnlocked;

        protected AbstractRequirementStrategy(
            AchievementModel achievementModel)
        {
            _achievementModel = achievementModel;
            _onAchievementUnlocked = new Subject<AchievementId>().AddTo(Disposer);
        }

        protected void UnlockAchievement()
        {
            _achievementModel.SetIsUnlocked(true);
            _onAchievementUnlocked.OnNext(_achievementModel.Id);
        }
    }
}
