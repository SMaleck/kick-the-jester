using Assets.Source.Features.Achievements;
using UniRx;

namespace Assets.Source.Services.Savegames.Models
{
    public class AchievementSavegameData : AbstractSavegameData
    {
        public int Id;
        public bool IsUnlocked;
    }

    public class AchievementSavegame : AbstractSavegame
    {
        public readonly AchievementId Id;
        public readonly ReactiveProperty<bool> IsUnlocked;

        public AchievementSavegame(AchievementSavegameData data)
        {
            Id = (AchievementId)data.Id;

            IsUnlocked = CreateBoundProperty(
                data.IsUnlocked,
                value => { data.IsUnlocked = value; },
                Disposer);
        }
    }
}
