using Assets.Source.Features.Upgrades;
using Assets.Source.Features.Upgrades.Data;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
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
        private readonly UpgradeController _upgradeController;

        public ShopController(
            ShopView view,
            ShopModel shopModel,
            RoundEndModel roundEndModel,
            ProfileModel profileModel,
            UpgradeController upgradeController)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _roundEndModel = roundEndModel;
            _shopModel = shopModel;
            _profileModel = profileModel;
            _upgradeController = upgradeController;

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
                .Subscribe(_ => _upgradeController.TryUpgrade(UpgradePathType.VelocityCap, _profileModel.Currency))
                .AddTo(Disposer);

            _view.OnKickForceLevelUp
                .Subscribe(_ => _upgradeController.TryUpgrade(UpgradePathType.KickForce, _profileModel.Currency))
                .AddTo(Disposer);

            _view.OnShootForceLevelUp
                .Subscribe(_ => _upgradeController.TryUpgrade(UpgradePathType.ShootForce, _profileModel.Currency))
                .AddTo(Disposer);

            _view.OnShootCountLevelUp
                .Subscribe(_ => _upgradeController.TryUpgrade(UpgradePathType.ProjectileCount, _profileModel.Currency))
                .AddTo(Disposer);
        }

        private void SetupProfileModel()
        {
            _profileModel.Currency
                .Subscribe(value =>
                {
                    _view.Currency = value;

                    UpdateMaxVelocityValues();
                    UpdateKickForceValues();
                    UpdateShootForceValues();
                    UpdateShootCountValues();
                })
                .AddTo(Disposer);
        }

        private void SetupUpgradesModel()
        {
            var velocityCapUpgradeModel = _upgradeController.GetUpgradeModel(UpgradePathType.VelocityCap);
            velocityCapUpgradeModel.Level
                .Subscribe(_ => UpdateMaxVelocityValues())
                .AddTo(Disposer);

            var kickForceUpgradeModel = _upgradeController.GetUpgradeModel(UpgradePathType.KickForce);
            kickForceUpgradeModel.Level
                .Subscribe(_ => UpdateKickForceValues())
                .AddTo(Disposer);

            var shootForceUpgradeModel = _upgradeController.GetUpgradeModel(UpgradePathType.ShootForce);
            shootForceUpgradeModel.Level
                .Subscribe(_ => UpdateShootForceValues())
                .AddTo(Disposer);

            var projectileCountUpgradeModel = _upgradeController.GetUpgradeModel(UpgradePathType.ProjectileCount);
            projectileCountUpgradeModel.Level
                .Subscribe(_ => UpdateShootCountValues())
                .AddTo(Disposer);
        }


        private void UpdateMaxVelocityValues()
        {
            var upgradeModel = _upgradeController.GetUpgradeModel(UpgradePathType.VelocityCap);

            var level = upgradeModel.Level.Value;
            var isAtMaxLevel = upgradeModel.IsMaxed.Value;
            var cost = upgradeModel.Cost.Value;

            _view.MaxVelocityLevel = level;
            _view.MaxVelocityCost = cost;
            _view.MaxVelocityCanAfford = !isAtMaxLevel && cost <= _profileModel.Currency.Value;
        }

        private void UpdateKickForceValues()
        {
            var upgradeModel = _upgradeController.GetUpgradeModel(UpgradePathType.KickForce);

            var level = upgradeModel.Level.Value;
            var isAtMaxLevel = upgradeModel.IsMaxed.Value;
            var cost = upgradeModel.Cost.Value;

            _view.KickForceLevel = level;
            _view.KickForceCost = cost;
            _view.KickForceCanAfford = !isAtMaxLevel && cost <= _profileModel.Currency.Value;
        }

        private void UpdateShootForceValues()
        {
            var upgradeModel = _upgradeController.GetUpgradeModel(UpgradePathType.ShootForce);

            var level = upgradeModel.Level.Value;
            var isAtMaxLevel = upgradeModel.IsMaxed.Value;
            var cost = upgradeModel.Cost.Value;

            _view.ShootForceLevel = level;
            _view.ShootForceCost = cost;
            _view.ShootForceCanAfford = !isAtMaxLevel && cost <= _profileModel.Currency.Value;
        }

        private void UpdateShootCountValues()
        {
            var upgradeModel = _upgradeController.GetUpgradeModel(UpgradePathType.ProjectileCount);

            var level = upgradeModel.Level.Value;
            var isAtMaxLevel = upgradeModel.IsMaxed.Value;
            var cost = upgradeModel.Cost.Value;

            _view.ShootCountLevel = level;
            _view.ShootCountCost = cost;
            _view.ShootCountCanAfford = !isAtMaxLevel && cost <= _profileModel.Currency.Value;
        }
    }
}
