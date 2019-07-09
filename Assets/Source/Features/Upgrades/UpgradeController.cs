using Assets.Source.Features.Upgrades.Data;
using Assets.Source.Services.Savegame;
using Assets.Source.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Source.Services.Upgrade;
using UniRx;

namespace Assets.Source.Features.Upgrades
{
    public class UpgradeController
    {
        private readonly SavegameService _savegameService;
        private readonly UpgradeTreeConfig _upgradeTreeConfig;

        private readonly Dictionary<UpgradePathType, UpgradeModel> _upgradeModels;

        public UpgradeController(
            SavegameService savegameService,
            UpgradeTreeConfig upgradeTreeConfig)
        {
            _savegameService = savegameService;
            _upgradeTreeConfig = upgradeTreeConfig;

            _upgradeModels = CreateUpgradeModels();
        }

        public UpgradeModel GetUpgradeModel(UpgradePathType upgradePathType)
        {
            return _upgradeModels[upgradePathType];
        }

        public bool TryUpgrade(UpgradePathType upgradePathType, IntReactiveProperty currency)
        {
            var upgradeModel = GetUpgradeModel(upgradePathType);

            if (upgradeModel.IsMaxed.Value)
            {
                return false;
            }

            var cost = upgradeModel.Cost.Value;
            if (!TryDeductCost(currency, cost))
            {
                return false;
            }

            var nextLevel = upgradeModel.Level.Value + 1;
            upgradeModel.SetLevel(nextLevel);

            return true;
        }


        private bool TryDeductCost(IntReactiveProperty currency, int cost)
        {
            if (currency.Value < cost)
            {
                return false;
            }

            currency.Value -= cost;
            return true;
        }

        private Dictionary<UpgradePathType, UpgradeModel> CreateUpgradeModels()
        {
            var upgradeModels = new Dictionary<UpgradePathType, UpgradeModel>();

            EnumHelper<UpgradePathType>.Iterator.ToList()
                .ForEach(upgradePath =>
                {
                    var upgradeModel = CreateUpgradeModel(upgradePath);
                    upgradeModels.Add(upgradePath, upgradeModel);
                });

            return upgradeModels;
        }

        private UpgradeModel CreateUpgradeModel(UpgradePathType upgradePath)
        {
            return new UpgradeModel(
                upgradePath,
                _upgradeTreeConfig,
                GetLevelSavegame(upgradePath));
        }

        private ReactiveProperty<int> GetLevelSavegame(UpgradePathType upgradePath)
        {
            var upgradeSavegame = _savegameService.Upgrades;

            switch (upgradePath)
            {
                case UpgradePathType.KickForce:
                    return upgradeSavegame.KickForceLevel;

                case UpgradePathType.ShootForce:
                    return upgradeSavegame.ShootForceLevel;

                case UpgradePathType.ProjectileCount:
                    return upgradeSavegame.ShootCountLevel;

                case UpgradePathType.VelocityCap:
                    return upgradeSavegame.MaxVelocityLevel;

                default:
                    throw new ArgumentOutOfRangeException(nameof(upgradePath), upgradePath, null);
            }
        }
    }
}
