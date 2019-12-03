using Assets.Source.Features.Achievements.Data;
using Assets.Source.Services.Savegames.Models;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Features.Achievements
{
    public class AchievementModel : AbstractDisposable
    {
        private readonly AchievementSavegame _achievementSavegame;
        private readonly IAchievementData _achievementData;

        public AchievementId Id => _achievementSavegame.Id;
        public IReadOnlyReactiveProperty<bool> IsOwned => _achievementSavegame.IsOwned;

        public readonly AchievementRequirementType RequirementType;
        public readonly double Requirement;

        public AchievementModel(
            AchievementSavegame achievementSavegame,
            IAchievementData achievementData)
        {
            _achievementSavegame = achievementSavegame;
            _achievementData = achievementData;

            RequirementType = _achievementData.GetRequirementType(Id);
            Requirement = _achievementData.GetRequirement(Id);
        }

        public void SetIsOwned(bool value)
        {
            _achievementSavegame.IsOwned.Value = true;
        }
    }
}
