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

            playerProfile.RP_MaxVelocityLevel.Subscribe((int value) => SetCostFor(value, UpgradeTree.MaxVelocityPath, MaxVelocityCost));
            playerProfile.RP_KickForceLevel.Subscribe((int value) => SetCostFor(value, UpgradeTree.KickForcePath, KickForceCost));
            playerProfile.RP_ShootForceLevel.Subscribe((int value) => SetCostFor(value, UpgradeTree.ShootForcePath, ShootForceCost));
            playerProfile.RP_ShootCountLevel.Subscribe((int value) => SetCostFor(value, UpgradeTree.ShootCountPath, ShootCountCost));
        }

        /// <summary>
        /// Upgrades a level if possible, deducting the corresponding cost.
        /// </summary>
        private void StatUp<T>(IntReactiveProperty RP_CurrentLevel, UpgradePath<T> upgradePath)
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

        private void SetCostFor<T>(int currentLevel, UpgradePath<T> upgradePath, IntReactiveProperty costProperty)
        {
            costProperty.Value = upgradePath.UpgradeCost(currentLevel);
        }

        /* -------------------------------------------------------------------------- */
        #region UPGRADE STATS INTERFACE

        public void MaxVelocityUp()
        {
            StatUp(playerProfile.RP_MaxVelocityLevel, UpgradeTree.MaxVelocityPath);                       
        }

        public void KickForceUp()
        {
            StatUp(playerProfile.RP_KickForceLevel, UpgradeTree.KickForcePath);            
        }

        public void ShootForceUp()
        {
            StatUp(playerProfile.RP_ShootForceLevel, UpgradeTree.ShootForcePath);
        }

        public void ShootCountUp()
        {
            StatUp(playerProfile.RP_ShootCountLevel, UpgradeTree.ShootCountPath);
        }

        #endregion
    }
}
