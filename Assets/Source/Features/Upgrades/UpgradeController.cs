using Assets.Source.Features.PlayerData;
using Assets.Source.Features.Upgrades.Data;
using Assets.Source.Services.Savegames;
using Assets.Source.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Assets.Source.Features.Upgrades
{
    public class UpgradeController : AbstractDisposable
    {
        private readonly ISavegameService _savegameService;
        private readonly UpgradeTreeConfig _upgradeTreeConfig;
        private readonly PlayerProfileModel _playerProfileModel;

        private readonly Dictionary<UpgradePathType, UpgradeModel> _upgradeModels;

        public UpgradeController(
            ISavegameService savegameService,
            UpgradeTreeConfig upgradeTreeConfig,
            PlayerProfileModel playerProfileModel)
        {
            _savegameService = savegameService;
            _upgradeTreeConfig = upgradeTreeConfig;
            _playerProfileModel = playerProfileModel;

            _upgradeModels = CreateUpgradeModels();

            _upgradeModels.Values
                .ToList()
                .ForEach(model =>
                {
                    model.Cost
                        .Subscribe(_ => UpdateCanAfford(model))
                        .AddTo(Disposer);
                });

            _playerProfileModel.CurrencyAmount
                .Subscribe(_ => UpdateAllCanAfford())
                .AddTo(Disposer);
        }

        public UpgradeModel GetUpgradeModel(UpgradePathType upgradePathType)
        {
            return _upgradeModels[upgradePathType];
        }

        public bool TryUpgrade(UpgradePathType upgradePathType)
        {
            var upgradeModel = GetUpgradeModel(upgradePathType);

            if (upgradeModel.IsMaxed.Value)
            {
                return false;
            }

            var cost = upgradeModel.Cost.Value;
            if (!TryDeductCost(cost))
            {
                return false;
            }

            var nextLevel = upgradeModel.Level.Value + 1;
            upgradeModel.SetLevel(nextLevel);

            return true;
        }


        private bool TryDeductCost(int cost)
        {
            if (_playerProfileModel.CurrencyAmount.Value < cost)
            {
                return false;
            }

            _playerProfileModel.DeductCurrencyAmount(cost);
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
                    GetLevelSavegame(upgradePath))
                .AddTo(Disposer);
        }

        private ReactiveProperty<int> GetLevelSavegame(UpgradePathType upgradePath)
        {
            var upgradesSavegame = _savegameService.Savegame.UpgradesSavegame;

            switch (upgradePath)
            {
                case UpgradePathType.KickForce:
                    return upgradesSavegame.KickForceLevel;

                case UpgradePathType.ShootForce:
                    return upgradesSavegame.ShootForceLevel;

                case UpgradePathType.ProjectileCount:
                    return upgradesSavegame.ShootCountLevel;

                case UpgradePathType.VelocityCap:
                    return upgradesSavegame.MaxVelocityLevel;

                default:
                    throw new ArgumentOutOfRangeException(nameof(upgradePath), upgradePath, null);
            }
        }

        private void UpdateAllCanAfford()
        {
            _upgradeModels.Values
                .ToList()
                .ForEach(UpdateCanAfford);
        }

        private void UpdateCanAfford(UpgradeModel model)
        {
            var currency = _playerProfileModel.CurrencyAmount.Value;
            var cost = model.Cost.Value;

            model.SetCanAfford(currency >= cost);
        }
    }
}
