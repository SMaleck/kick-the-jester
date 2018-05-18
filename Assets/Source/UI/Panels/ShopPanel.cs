using Assets.Source.App;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Text txtMoney;

        [SerializeField] private Button kickForceUpgradeButton;
        [SerializeField] private Button kickCountUpgradeButton;
        [SerializeField] private Button statResetButton;


        private void Start()
        {
            backButton.OnClickAsObservable().Subscribe(_ => Kernel.SceneTransitionService.ToGame());

            kickForceUpgradeButton.OnClickAsObservable().Subscribe(_ => OnKickForceUpgrade());
            kickCountUpgradeButton.OnClickAsObservable().Subscribe(_ => OnKickCountUpgrade());
            statResetButton.OnClickAsObservable().Subscribe(_ => OnStatReset());

            Kernel.PlayerProfileService.currencyProperty.Subscribe((int value) => { OnCurrencyChanged(value); }).AddTo(this);
        }


        private void OnCurrencyChanged(int value)
        {
            txtMoney.text = value + "G";
        }


        public void OnKickForceUpgrade()
        {
            if (TryDeduct(200))
            {
                Kernel.PlayerProfileService.KickForce += 200f;
            }
        }


        public void OnKickCountUpgrade()
        {
            if (TryDeduct(500))
            {
                int kickCount = Kernel.PlayerProfileService.KickCount;
                Kernel.PlayerProfileService.KickCount = kickCount + 2;
            }
        }


        public void OnStatReset()
        {
            Kernel.PlayerProfileService.ResetStats();
        }


        /// <summary>
        /// Deducts Currency from the PlayerProfile, if there is enough
        /// </summary>
        /// <param name="amount">The amount to deduct, ABS amount will be processed</param>
        /// <returns></returns>
        public bool TryDeduct(int amount)
        {
            if (!Kernel.PlayerProfileService.IsLoaded)
            {
                return false;
            }

            // Check if player has enough money
            if (Kernel.PlayerProfileService.Currency >= amount)
            {
                Kernel.PlayerProfileService.Currency -= Math.Abs(amount);
            }

            return true;
        }
    }
}
