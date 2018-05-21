using Assets.Source.App.Storage;

namespace Assets.Source.App.Upgrades
{
    public class UpgradeService
    {
        private readonly PlayerProfileService playerProfile;

        public UpgradeService(PlayerProfileService playerProfile)
        {
            this.playerProfile = playerProfile;
        }


        public void MaxVelocityUp()
        {
            if(playerProfile.RP_MaxVelocityLevel.Value < UpgradeTree.MaxVelocity.Length)
            {
                playerProfile.RP_MaxVelocityLevel.Value += 1;
            }            
        }


        public void KickForceUp()
        {
            if (playerProfile.RP_KickForceLevel.Value < UpgradeTree.KickForce.Length)
            {
                playerProfile.RP_KickForceLevel.Value += 1;
            }
        }


        public void ShootForceUp()
        {
            if (playerProfile.RP_ShootForceLevel.Value < UpgradeTree.ShootForce.Length)
            {
                playerProfile.RP_ShootForceLevel.Value += 1;
            }
        }


        public void ShootCountUp()
        {
            if (playerProfile.RP_ShootCountLevel.Value < UpgradeTree.ShootCount.Length)
            {
                playerProfile.RP_ShootCountLevel.Value += 1;
            }
        }
    }
}
