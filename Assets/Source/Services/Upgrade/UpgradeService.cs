using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.App.Persistence;
using UniRx;

namespace Assets.Source.Services.Upgrade
{
    public class UpgradeService
    {        
        public bool TryUpgrade<T>(IntReactiveProperty currentLevel, IntReactiveProperty currency, UpgradePath<T> upgradePath)
        {
            if (upgradePath.IsMaxLevel(currentLevel.Value))
            {
                return false;
            }

            var cost = upgradePath.UpgradeCost(currentLevel.Value);
            if (!TryDeductCost(currency, cost))
            {
                return false;
            }

            currentLevel.Value = upgradePath.NextLevel(currentLevel.Value);
            return true;
        }


        private bool TryDeductCost(IntReactiveProperty currency, int cost)
        {
            if (currency.Value < cost)
            {
                return false;
            }

            currency.Value -= cost;
            return true;
        }
    }
}
