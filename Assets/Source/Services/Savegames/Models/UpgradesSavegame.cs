using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Assets.Source.Services.Savegames.Models
{
    public class UpgradesSavegameData : AbstractSavegameData
    {
        public List<UpgradeSavegameData> UpgradeSavegames;
    }

    public class UpgradesSavegame : AbstractSavegame
    {
        public readonly ReactiveCollection<UpgradeSavegame> UpgradeSavegames;

        public UpgradesSavegame(UpgradesSavegameData data)
        {
            var upgradeSavegamesList = data.UpgradeSavegames
                .Select(CreateUpgradeSavegame);

            UpgradeSavegames = new ReactiveCollection<UpgradeSavegame>(upgradeSavegamesList);
        }

        private UpgradeSavegame CreateUpgradeSavegame(UpgradeSavegameData data)
        {
            return new UpgradeSavegame(data);
        }
    }
}
