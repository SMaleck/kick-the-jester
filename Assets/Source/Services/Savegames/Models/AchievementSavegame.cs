using Assets.Source.Features.Achievements;
using UniRx;

namespace Assets.Source.Services.Savegames.Models
{
    public class AchievementSavegameData : AbstractSavegameData
    {
        public int Id;
        public bool IsOwned;
    }

    public class AchievementSavegame : AbstractSavegame
    {
        public readonly AchievementId Id;
        public readonly ReactiveProperty<bool> IsOwned;

        public AchievementSavegame(AchievementSavegameData data)
        {
            Id = (AchievementId)data.Id;

            IsOwned = CreateBoundProperty(
                data.IsOwned,
                value => { data.IsOwned = value; },
                Disposer);
        }
    }
}
