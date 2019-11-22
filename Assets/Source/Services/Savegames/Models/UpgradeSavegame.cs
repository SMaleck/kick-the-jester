using Assets.Source.Features.Upgrades.Data;
using UniRx;

namespace Assets.Source.Services.Savegames.Models
{
    public class UpgradeSavegameData : AbstractSavegameData
    {
        public int UpgradePathType;
        public int Level;
    }

    public class UpgradeSavegame : AbstractSavegame
    {
        public readonly ReactiveProperty<UpgradePathType> UpgradePathType;
        public readonly ReactiveProperty<int> Level;

        public UpgradeSavegame(UpgradeSavegameData data)
        {
            UpgradePathType = CreateBoundProperty(
                (UpgradePathType)data.UpgradePathType,
                value => { data.UpgradePathType = (int)value; },
                Disposer);

            Level = CreateBoundProperty(
                data.Level,
                value => { data.Level = value; },
                Disposer);
        }
    }
}
