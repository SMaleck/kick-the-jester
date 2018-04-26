using Assets.Source.GameLogic;
using Assets.Source.Repositories;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Assets.Source.UI
{
    public class ShopItemUI : MonoBehaviour
    {
        public Text txtMoney;

        private void Awake()
        {
            App.Cache.playerProfile.currencyProperty
                .TakeUntilDestroy(this)
                .Subscribe((int value) => { UpdateCurrency(value); });
        }

        private void UpdateCurrency(int currency)
        {
            if(txtMoney != null)
            {
                txtMoney.text = currency + "G";
            }
        }

        public void PurchaseKickForceUpgrade()
        {
            if (TryDeduct(200))
            {
                App.Cache.playerProfile.KickForce += 200f;
            }            
        }

        public void PurchaseKickCountUpgrade()
        {
            if (TryDeduct(500))
            {
                int kickCount = App.Cache.playerProfile.KickCount;
                App.Cache.playerProfile.KickCount = kickCount + 2;
            }            
        }

        public void PurchaseStatsReset()
        {
            App.Cache.playerProfile.ResetStats();
        }


        /// <summary>
        /// Deducts Currency from the PlayerProfile, if there is enough
        /// </summary>
        /// <param name="amount">The amount to deduct, ABS amount will be processed</param>
        /// <returns></returns>
        public bool TryDeduct(int amount)
        {
            if (!App.Cache.playerProfile.IsLoaded)
            {
                return false;
            }

            // Check if player has enough money
            if (App.Cache.playerProfile.Currency >= amount)
            {
                App.Cache.playerProfile.Currency -= Math.Abs(amount);
            }

            return true;
        }
    }
}
