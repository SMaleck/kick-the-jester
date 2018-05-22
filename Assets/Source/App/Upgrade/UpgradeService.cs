using Assets.Source.App.Storage;
using System;
using System.Collections.Generic;
using System.Collections;
using UniRx;

namespace Assets.Source.App.Upgrade
{
    public class UpgradeService
    {
        private readonly PlayerProfileService playerProfile;

        public UpgradeService(PlayerProfileService playerProfile)
        {
            this.playerProfile = playerProfile;
        }

        /// <summary>
        /// Upgrades a level if possible, deducting the corresponding cost.
        /// </summary>
        private void PurchaseNextLevel<T>(IntReactiveProperty RP_CurrentLevel, UpgradePath<T> upgradePath)
        {
            int currentLevel = RP_CurrentLevel.Value;

            if (upgradePath.IsMaxLevel(currentLevel)) return;

            if (TryDeductCost(upgradePath.UpgradeCost(currentLevel)))
            {
                RP_CurrentLevel.Value = upgradePath.NextLevel(currentLevel);
            }
        }
        
        /// <summary>
        /// <para>Attempts to deduct the cost of the Upgrade from the players currency.</para>
        /// Returns false, if there isn't enough money
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="upgradeCost"></param>
        /// <returns></returns>
        private bool TryDeductCost(int upgradeCost)
        {
            if (playerProfile.Currency < upgradeCost)
            {
                return false;
            }

            playerProfile.Currency -= upgradeCost;
            return true;
        }

        /* -------------------------------------------------------------------------- */
        #region UPGRADE STATS INTERFACE

        public void MaxVelocityUp()
        {
            PurchaseNextLevel(playerProfile.RP_MaxVelocityLevel, UpgradeTree.MaxVelocityPath);                       
        }

        public void KickForceUp()
        {
            PurchaseNextLevel(playerProfile.RP_KickForceLevel, UpgradeTree.KickForcePath);            
        }

        public void ShootForceUp()
        {
            PurchaseNextLevel(playerProfile.RP_ShootForceLevel, UpgradeTree.ShootForcePath);
        }

        public void ShootCountUp()
        {
            PurchaseNextLevel(playerProfile.RP_ShootCountLevel, UpgradeTree.ShootCountPath);
        }

        #endregion
    }
}
