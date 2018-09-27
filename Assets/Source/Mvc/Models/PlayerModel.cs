using Assets.Source.Services.Savegame;
using Assets.Source.Services.Upgrade;

namespace Assets.Source.Mvc.Models
{
    public class PlayerModel
    {        
        public float KickForce;
        public float ShootForce;
        public int Shots;

        public float MaxVelocityX;
        public float MaxVelocityY;


        public PlayerModel(SavegameService savegameService)
        {
            var upgrades = savegameService.Upgrades;

            KickForce = UpgradeTree.KickForcePath.ValueAt(upgrades.KickForceLevel.Value);
            ShootForce = UpgradeTree.ShootForcePath.ValueAt(upgrades.ShootForceLevel.Value);
            Shots = UpgradeTree.ShootCountPath.ValueAt(upgrades.ShootCountLevel.Value);
            MaxVelocityX = UpgradeTree.MaxVelocityPath.ValueAt(upgrades.MaxVelocityLevel.Value);
        }
    }
}
