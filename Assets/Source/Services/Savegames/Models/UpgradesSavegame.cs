using UniRx;

namespace Assets.Source.Services.Savegames.Models
{
    public class UpgradesSavegameData : AbstractSavegameData
    {
        public int MaxVelocityLevel;
        public int KickForceLevel;
        public int ShootForceLevel;
        public int ShootCountLevel;
    }

    public class UpgradesSavegame : AbstractSavegame
    {
        public readonly ReactiveProperty<int> MaxVelocityLevel;
        public readonly ReactiveProperty<int> KickForceLevel;
        public readonly ReactiveProperty<int> ShootForceLevel;
        public readonly ReactiveProperty<int> ShootCountLevel;

        public UpgradesSavegame(UpgradesSavegameData data)
        {
            MaxVelocityLevel = CreateBoundProperty(
                data.MaxVelocityLevel,
                value => { data.MaxVelocityLevel = value; },
                Disposer);

            KickForceLevel = CreateBoundProperty(
                data.KickForceLevel,
                value => { data.KickForceLevel = value; },
                Disposer);

            ShootForceLevel = CreateBoundProperty(
                data.ShootForceLevel,
                value => { data.ShootForceLevel = value; },
                Disposer);

            ShootCountLevel = CreateBoundProperty(
                data.ShootCountLevel,
                value => { data.ShootCountLevel = value; },
                Disposer);
        }
    }
}
