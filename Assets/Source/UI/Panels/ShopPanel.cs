﻿using Assets.Source.App;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Text txtMoney;
        [SerializeField] private Button statResetButton;

        // Max Velocity
        [SerializeField] private Text maxVelocityLevel;
        [SerializeField] private Text maxVelocityCost;
        [SerializeField] private Button maxVelocityUp;

        // Kick Force
        [SerializeField] private Text kickForceLevel;
        [SerializeField] private Text kickForceCost;
        [SerializeField] private Button kickForceUp;

        // Shoot Force
        [SerializeField] private Text shootForceLevel;
        [SerializeField] private Text shootForceCost;
        [SerializeField] private Button shootForceUp;

        // Shoot Force
        [SerializeField] private Text shootCountLevel;
        [SerializeField] private Text shootCountCost;
        [SerializeField] private Button shootCountUp;

        
        private void Start()
        {
            // Correct own position, because the spawning tends to screw it up
            transform.localPosition = new Vector3(0, 0, 0);

            backButton.OnClickAsObservable().Subscribe(_ => Kernel.SceneTransitionService.ToGame());

            Kernel.PlayerProfileService.RP_Currency
                                       .SubscribeToText(txtMoney, e => e.ToString())
                                       .AddTo(this);

            // Upgrades
            maxVelocityUp.OnClickAsObservable().Subscribe(_ => Kernel.UpgradeService.MaxVelocityUp()).AddTo(this);
            Kernel.PlayerProfileService.RP_MaxVelocityLevel.SubscribeToText(maxVelocityLevel).AddTo(this);
            Kernel.UpgradeService.MaxVelocityCost.Subscribe((int value) => UpdateCost(value, maxVelocityCost, maxVelocityUp)).AddTo(this);

            kickForceUp.OnClickAsObservable().Subscribe(_ => Kernel.UpgradeService.KickForceUp()).AddTo(this);
            Kernel.PlayerProfileService.RP_KickForceLevel.SubscribeToText(kickForceLevel).AddTo(this);
            Kernel.UpgradeService.KickForceCost.Subscribe((int value) => UpdateCost(value, kickForceCost, kickForceUp)).AddTo(this);

            shootForceUp.OnClickAsObservable().Subscribe(_ => Kernel.UpgradeService.ShootForceUp()).AddTo(this);
            Kernel.PlayerProfileService.RP_ShootForceLevel.SubscribeToText(shootForceLevel).AddTo(this);
            Kernel.UpgradeService.ShootForceCost.Subscribe((int value) => UpdateCost(value, shootForceCost, shootForceUp)).AddTo(this);

            shootCountUp.OnClickAsObservable().Subscribe(_ => Kernel.UpgradeService.ShootCountUp()).AddTo(this);
            Kernel.PlayerProfileService.RP_ShootCountLevel.SubscribeToText(shootCountLevel).AddTo(this);
            Kernel.UpgradeService.ShootCountCost.Subscribe((int value) => UpdateCost(value, shootCountCost, shootCountUp)).AddTo(this);

            // Stat Reset
            statResetButton.OnClickAsObservable().Subscribe(_ => Kernel.PlayerProfileService.ResetStats());
        }


        private void UpdateCost(int cost, Text label, Button buy)
        {
            // Zero means MAX level is reached
            if(cost <= 0)
            {
                label.text = "- - -";
                return;
            }

            label.text = cost.ToString();
            buy.interactable = cost <= Kernel.PlayerProfileService.Currency;
        }
    }
}
