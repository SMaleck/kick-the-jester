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
        [SerializeField] private Button shootForceUpButton;
        [SerializeField] private Button shootCountUpButton;

        [SerializeField] private Button statResetButton;


        private void Start()
        {
            backButton.OnClickAsObservable().Subscribe(_ => Kernel.SceneTransitionService.ToGame());

            Kernel.PlayerProfileService.RP_Currency
                                       .SubscribeToText(txtMoney, e => e.ToString())
                                       .AddTo(this);

            kickForceUpgradeButton.OnClickAsObservable().Subscribe(_ => OnKickForceUp());
            kickCountUpgradeButton.OnClickAsObservable().Subscribe(_ => OnShootCountUp());

            statResetButton.OnClickAsObservable().Subscribe(_ => OnStatReset());            
        }


        /* UPGRADE BUTTONS */

        public void OnMaxVelocityUp()
        {
            if (TryDeduct(200))
            {
                Kernel.UpgradeService.MaxVelocityUp();
            }
        }

        public void OnKickForceUp()
        {
            if (TryDeduct(200))
            {
                Kernel.UpgradeService.KickForceUp();
            }
        }

        public void OnShootForceUp()
        {
            if (TryDeduct(500))
            {
                Kernel.UpgradeService.ShootForceUp();
            }
        }

        public void OnShootCountUp()
        {
            if (TryDeduct(500))
            {
                Kernel.UpgradeService.ShootCountUp();
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
            // Check if player has enough money
            if (Kernel.PlayerProfileService.Currency >= amount)
            {
                Kernel.PlayerProfileService.Currency -= Math.Abs(amount);
            }

            return true;
        }
    }
}
