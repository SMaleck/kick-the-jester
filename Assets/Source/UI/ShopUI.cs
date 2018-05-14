using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Text txtMoney;

        [SerializeField] private Button kickForceUpgradeButton;
        [SerializeField] private Button kickCountUpgradeButton;
        [SerializeField] private Button statResetButton;


        private void Start()
        {
            backButton.OnClickAsObservable().Subscribe(_ => App.Cache.Services.SceneTransitionService.ToGame());

            kickForceUpgradeButton.OnClickAsObservable().Subscribe(_ => OnKickForceUpgrade());
            kickCountUpgradeButton.OnClickAsObservable().Subscribe(_ => OnKickCountUpgrade());
            statResetButton.OnClickAsObservable().Subscribe(_ => OnStatReset());

            App.Cache.playerProfile.currencyProperty.Subscribe((int value) => { OnCurrencyChanged(value); });
        }


        private void OnCurrencyChanged(int value)
        {
            txtMoney.text = value + "G";
        }


        public void OnKickForceUpgrade()
        {
            if (TryDeduct(200))
            {
                App.Cache.playerProfile.KickForce += 200f;
            }
        }


        public void OnKickCountUpgrade()
        {
            if (TryDeduct(500))
            {
                int kickCount = App.Cache.playerProfile.KickCount;
                App.Cache.playerProfile.KickCount = kickCount + 2;
            }
        }


        public void OnStatReset()
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
