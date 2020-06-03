using Assets.Source.Features.Achievements.Data;
using Assets.Source.Services.Savegames.Models;
using Assets.Source.Util;
using System;
using UniRx;
using Zenject;

namespace Assets.Source.Features.Achievements
{
    public class AchievementModel : AbstractDisposable
    {
        public class Factory : PlaceholderFactory<AchievementSavegame, AchievementModel> { }

        private readonly AchievementSavegame _achievementSavegame;
        private readonly IAchievementData _achievementData;

        public AchievementId Id => _achievementSavegame.Id;
        public IReadOnlyReactiveProperty<bool> IsUnlocked => _achievementSavegame.IsUnlocked;

        public readonly AchievementRequirementType RequirementType;
        public readonly double Requirement;

        private IReactiveProperty<double> _requirementProgress;
        public IReadOnlyReactiveProperty<double> RequirementProgress => _requirementProgress;
        public IReadOnlyReactiveProperty<double> RelativeProgress;

        public AchievementModel(
            AchievementSavegame achievementSavegame,
            IAchievementData achievementData)
        {
            _achievementSavegame = achievementSavegame;
            _achievementData = achievementData;

            RequirementType = _achievementData.GetRequirementType(Id);
            Requirement = _achievementData.GetRequirement(Id);
            _requirementProgress = new ReactiveProperty<double>().AddTo(Disposer);

            RelativeProgress = _requirementProgress
                .Select(value => Math.Max(1, value / Requirement))
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposer);
        }

        public void SetIsUnlocked(bool value)
        {
            _achievementSavegame.IsUnlocked.Value = value;
        }

        public void SetRequirementProgress(double value)
        {
            _requirementProgress.Value = value;
        }
    }
}
