using Assets.Source.Services.Savegame;
using UniRx;

namespace Assets.Source.Mvc.Models
{
    public class UpgradesModel
    {
        public IntReactiveProperty MaxVelocityLevel;
        public IntReactiveProperty KickForceLevel;
        public IntReactiveProperty ShootForceLevel;
        public IntReactiveProperty ShootCountLevel;

        public UpgradesModel(SavegameService savegameService)
        {
            var upgrades = savegameService.Upgrades;

            MaxVelocityLevel = upgrades.MaxVelocityLevel;
            KickForceLevel = upgrades.KickForceLevel;
            ShootForceLevel = upgrades.ShootForceLevel;
            ShootCountLevel = upgrades.ShootCountLevel;
        }
    }
}
