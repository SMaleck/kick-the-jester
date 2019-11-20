using UniRx;

namespace Assets.Source.Services.Savegames.Models
{
    public class ProfileSavegameData : AbstractSavegameData
    {
        public bool IsFirstStart;
        public int Currency;
        public float BestDistance;
        public int RoundsPlayed;
    }

    public class ProfileSavegame : AbstractSavegame
    {
        public readonly ReactiveProperty<bool> IsFirstStart;
        public readonly ReactiveProperty<int> Currency;
        public readonly ReactiveProperty<float> BestDistance;
        public readonly ReactiveProperty<int> RoundsPlayed;

        public ProfileSavegame(ProfileSavegameData data)
        {
            IsFirstStart = CreateBoundProperty(
                data.IsFirstStart,
                value => { data.IsFirstStart = value; },
                Disposer);

            Currency = CreateBoundProperty(
                data.Currency,
                value => { data.Currency = value; },
                Disposer);

            BestDistance = CreateBoundProperty(
                data.BestDistance,
                value => { data.BestDistance = value; },
                Disposer);

            RoundsPlayed = CreateBoundProperty(
                data.RoundsPlayed,
                value => { data.RoundsPlayed = value; },
                Disposer);
        }
    }
}
