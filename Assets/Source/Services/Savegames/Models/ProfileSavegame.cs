using UniRx;

namespace Assets.Source.Services.Savegames.Models
{
    public class ProfileSavegameData : AbstractSavegameData
    {
        public bool IsFirstStart;
        public int Currency;
    }

    public class ProfileSavegame : AbstractSavegame
    {
        public readonly ReactiveProperty<bool> IsFirstStart;
        public readonly ReactiveProperty<int> Currency;

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
        }
    }
}
