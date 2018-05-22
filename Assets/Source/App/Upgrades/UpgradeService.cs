using Assets.Source.App.Storage;
using System.Collections.Generic;
using UniRx;

namespace Assets.Source.App.Upgrades
{
    public class UpgradeService
    {
        private readonly PlayerProfileService playerProfile;

        /* -------------------------------------------------------------------------- */
        #region UPGRADE STAT COSTS            

        public IntReactiveProperty MaxVelocityCost = new IntReactiveProperty(0);
        public IntReactiveProperty KickForceCost = new IntReactiveProperty(0);
        public IntReactiveProperty ShootForceCost = new IntReactiveProperty(0);
        public IntReactiveProperty ShootCountCost = new IntReactiveProperty(0);

        #endregion


        public UpgradeService(PlayerProfileService playerProfile)
        {
            this.playerProfile = playerProfile;

            playerProfile.RP_MaxVelocityLevel.Subscribe((int value) => SetCostFor(value, UpgradeTree.MaxVelocity, MaxVelocityCost));
            playerProfile.RP_KickForceLevel.Subscribe((int value) => SetCostFor(value, UpgradeTree.KickForce, KickForceCost));
            playerProfile.RP_ShootForceLevel.Subscribe((int value) => SetCostFor(value, UpgradeTree.ShootForce, ShootForceCost));
            playerProfile.RP_ShootCountLevel.Subscribe((int value) => SetCostFor(value, UpgradeTree.ShootCount, ShootCountCost));
        }


        // Upgrade a Stat
        // Cheks if max level was reached and deducts corresponding cost
        private void StatUp<T>(IntReactiveProperty level, List<UpgradeStep<T>> upgradePath)
        {
            // Check if stat is already at max Level
            if (level.Value >= upgradePath.Count - 1)
            {
                return;
            }

            int nextLevel = level.Value + 1;

            if (TryDeductCost(upgradePath[nextLevel]))
            {
                level.Value = nextLevel;
            }
        }


        // Attempts to dedutc the cost of the Upgrade from the players currency
        // Returns false, if there isn't enough money
        private bool TryDeductCost<T>(UpgradeStep<T> upgradeStep)
        {
            if (playerProfile.Currency < upgradeStep.Cost)
            {
                return false;
            }

            playerProfile.Currency -= upgradeStep.Cost;
            return true;
        }
        

        private void SetCostFor<T>(int currentLevel, List<UpgradeStep<T>> upgradePath, IntReactiveProperty costProperty)
        {
            // Set Value to zero, if max level is reached
            if(currentLevel >= upgradePath.Count -1)
            {
                costProperty.Value = 0;
                return;
            }

            costProperty.Value = upgradePath[currentLevel + 1].Cost;
        }


        /* -------------------------------------------------------------------------- */
        #region UPGRADE STATS INTERFACE

        public void MaxVelocityUp()
        {
            StatUp(playerProfile.RP_MaxVelocityLevel, UpgradeTree.MaxVelocity);                       
        }

        public void KickForceUp()
        {
            StatUp(playerProfile.RP_KickForceLevel, UpgradeTree.KickForce);            
        }

        public void ShootForceUp()
        {
            StatUp(playerProfile.RP_ShootForceLevel, UpgradeTree.ShootForce);
        }

        public void ShootCountUp()
        {
            StatUp(playerProfile.RP_ShootCountLevel, UpgradeTree.ShootCount);
        }

        #endregion
    }
}
