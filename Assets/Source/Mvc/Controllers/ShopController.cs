using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using Assets.Source.Services.Upgrade;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    // ToDo See Github Issue #92
    public class ShopController : ClosableController
    {
        private readonly ShopView _view;
        private readonly RoundEndModel _roundEndModel;
        private readonly ShopModel _shopModel;
        private readonly ProfileModel _profileModel;
        private readonly UpgradesModel _upgradesModel;
        private readonly UpgradeService _upgradeService;

        public ShopController(
            ShopView view, 
            ShopModel shopModel, 
            RoundEndModel roundEndModel, 
            ProfileModel profileModel, 
            UpgradesModel upgradesModel, 
            UpgradeService upgradeService)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _roundEndModel = roundEndModel;
            _shopModel = shopModel;
            _profileModel = profileModel;
            _upgradesModel = upgradesModel;
            _upgradeService = upgradeService;

            _roundEndModel.OpenShop
                .Subscribe(_ => Open())
                .AddTo(Disposer);

            SetupOnClick();
            SetupProfileModel();
            SetupUpgradesModel();            
        }

        private void SetupOnClick()
        {
            _view.OnResetClicked
                .Subscribe(_ => _shopModel.OpenConfirmReset.Execute())
                .AddTo(Disposer);

            _view.OnMaxVelocityLevelUp
                .Subscribe(_ => _upgradeService.TryUpgrade(_upgradesModel.MaxVelocityLevel, _profileModel.Currency, UpgradeTree.MaxVelocityPath))
                .AddTo(Disposer);

            _view.OnKickForceLevelUp
                .Subscribe(_ => _upgradeService.TryUpgrade(_upgradesModel.KickForceLevel, _profileModel.Currency, UpgradeTree.KickForcePath))
                .AddTo(Disposer);

            _view.OnShootForceLevelUp
                .Subscribe(_ => _upgradeService.TryUpgrade(_upgradesModel.ShootForceLevel, _profileModel.Currency, UpgradeTree.ShootForcePath))
                .AddTo(Disposer);

            _view.OnShootCountLevelUp
                .Subscribe(_ => _upgradeService.TryUpgrade(_upgradesModel.ShootCountLevel, _profileModel.Currency, UpgradeTree.ShootCountPath))
                .AddTo(Disposer);
        }

        private void SetupProfileModel()
        {
            _profileModel.Currency
                .Subscribe(value =>
                {
                    _view.Currency = value;

                    UpdateMaxVelocityValues();
                    UpdatKickForceValues();
                    UpdateShootForceValues();
                    UpdateShootCountValues();
                })
                .AddTo(Disposer);
        }

        private void SetupUpgradesModel()
        {
            _upgradesModel.MaxVelocityLevel
                .Subscribe(_ => UpdateMaxVelocityValues())
                .AddTo(Disposer);

            _upgradesModel.KickForceLevel
                .Subscribe(_ => UpdatKickForceValues())
                .AddTo(Disposer);

            _upgradesModel.ShootForceLevel
                .Subscribe(_ => UpdateShootForceValues())
                .AddTo(Disposer);

            _upgradesModel.ShootCountLevel
                .Subscribe(_ => UpdateShootCountValues())
                .AddTo(Disposer);
        }


        private void UpdateMaxVelocityValues()
        {
            var level = _upgradesModel.MaxVelocityLevel.Value;
            _view.MaxVelocityLevel = _upgradesModel.MaxVelocityLevel.Value;

            var cost = UpgradeTree.MaxVelocityPath.UpgradeCost(level);
            var isAtMaxLevel = level == UpgradeTree.MaxVelocityPath.MaxLevel;

            _view.MaxVelocityCost = cost;
            _view.MaxVelocityCanAfford = !isAtMaxLevel && cost <= _profileModel.Currency.Value;
        }

        private void UpdatKickForceValues()
        {
            var level = _upgradesModel.KickForceLevel.Value;
            _view.KickForceLevel = level;

            var cost = UpgradeTree.KickForcePath.UpgradeCost(level);
            var isAtMaxLevel = level == UpgradeTree.KickForcePath.MaxLevel;

            _view.KickForceCost = cost;
            _view.KickForceCanAfford = !isAtMaxLevel && cost <= _profileModel.Currency.Value;
        }

        private void UpdateShootForceValues()
        {
            var level = _upgradesModel.ShootForceLevel.Value;
            _view.ShootForceLevel = level;

            var cost = UpgradeTree.ShootForcePath.UpgradeCost(level);
            var isAtMaxLevel = level == UpgradeTree.ShootForcePath.MaxLevel;

            _view.ShootForceCost = cost;
            _view.ShootForceCanAfford = !isAtMaxLevel && cost <= _profileModel.Currency.Value;
        }

        private void UpdateShootCountValues()
        {
            var level = _upgradesModel.ShootCountLevel.Value;
            _view.ShootCountLevel = level;

            var cost = UpgradeTree.ShootCountPath.UpgradeCost(level);
            var isAtMaxLevel = level == UpgradeTree.ShootCountPath.MaxLevel;

            _view.ShootCountCost = cost;
            _view.ShootCountCanAfford = !isAtMaxLevel && cost <= _profileModel.Currency.Value;
        }
    }
}
