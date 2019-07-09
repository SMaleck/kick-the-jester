using Assets.Source.Features.Upgrades;
using Assets.Source.Features.Upgrades.Data;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Features.PlayerData
{
    public class PlayerAttributesController : AbstractDisposable
    {
        public PlayerAttributesController(
            PlayerAttributesModel playerAttributesModel,
            UpgradeController upgradeController)
        {
            var kickForceUpgrade = upgradeController.GetUpgradeModel(UpgradePathType.KickForce);
            kickForceUpgrade.Value
                .Subscribe(playerAttributesModel.SetKickForce)
                .AddTo(Disposer);

            var shootForceUpgrade = upgradeController.GetUpgradeModel(UpgradePathType.ShootForce);
            shootForceUpgrade.Value
                .Subscribe(playerAttributesModel.SetShootForce)
                .AddTo(Disposer);

            var projectileCountUpgrade = upgradeController.GetUpgradeModel(UpgradePathType.ProjectileCount);
            projectileCountUpgrade.Value
                .Subscribe(value => playerAttributesModel.SetShots((int)value))
                .AddTo(Disposer);

            var velocityCapUpgrade = upgradeController.GetUpgradeModel(UpgradePathType.VelocityCap);
            velocityCapUpgrade.Value
                .Subscribe(value =>
                {
                    playerAttributesModel.SetMaxVelocityX(value);
                    playerAttributesModel.SetMaxVelocityY(value);
                })
                .AddTo(Disposer);
        }
    }
}
