using Assets.Source.App;
using Assets.Source.App.Upgrade;
using Assets.Source.Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class ShopPanel : AbstractPanel
    {
        [Header("Panel Properties")]

        [SerializeField] private GameObject panelConfirmReset;

        [SerializeField] private Button closeButton;        
        [SerializeField] private Button statResetButton;
        [SerializeField] private Text txtMoney;        
        [SerializeField] private AudioClip sfxUpgradeClick;

        [Header("Upgrades")]

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


        public override void Setup()
        {
            base.Setup();            

            // Setup Confirm Panel
            panelConfirmReset.GetComponent<AbstractPanel>().Setup();

            closeButton.OnClickAsObservable().Subscribe(_ => Hide());

            App.Cache.Kernel.PlayerProfile.Stats.RP_Currency
                                      .Subscribe(OnCurrencyChange)                                       
                                      .AddTo(this);

            // Upgrades
            maxVelocityUp.OnClickAsObservable()
                         .Subscribe(_ => OnUpgradeButtonClick(App.Cache.Kernel.UpgradeService.MaxVelocityUp))
                         .AddTo(this);
            App.Cache.Kernel.PlayerProfile.Upgrades.RP_MaxVelocityLevel
                                         .SubscribeToText(maxVelocityLevel)
                                         .AddTo(this);
            App.Cache.Kernel.PlayerProfile.Upgrades.RP_MaxVelocityLevel
                                         .Subscribe(level => UpdateUI(UpgradeTree.MaxVelocityPath.UpgradeCost(level), maxVelocityCost, maxVelocityUp))
                                         .AddTo(this);
            
            kickForceUp.OnClickAsObservable()
                       .Subscribe(_ => OnUpgradeButtonClick(App.Cache.Kernel.UpgradeService.KickForceUp))
                       .AddTo(this);
            App.Cache.Kernel.PlayerProfile.Upgrades.RP_KickForceLevel.SubscribeToText(kickForceLevel).AddTo(this);
            App.Cache.Kernel.PlayerProfile.Upgrades.RP_KickForceLevel
                                         .Subscribe(level => UpdateUI(UpgradeTree.KickForcePath.UpgradeCost(level), kickForceCost, kickForceUp))
                                         .AddTo(this);

            shootForceUp.OnClickAsObservable()
                        .Subscribe(_ => OnUpgradeButtonClick(App.Cache.Kernel.UpgradeService.ShootForceUp))
                        .AddTo(this);
            App.Cache.Kernel.PlayerProfile.Upgrades.RP_ShootForceLevel.SubscribeToText(shootForceLevel).AddTo(this);
            App.Cache.Kernel.PlayerProfile.Upgrades.RP_ShootForceLevel
                                .Subscribe(level => UpdateUI(UpgradeTree.ShootForcePath.UpgradeCost(level), shootForceCost, shootForceUp))
                                .AddTo(this);

            shootCountUp.OnClickAsObservable()
                        .Subscribe(_ => OnUpgradeButtonClick(App.Cache.Kernel.UpgradeService.ShootCountUp))
                        .AddTo(this);
            App.Cache.Kernel.PlayerProfile.Upgrades.RP_ShootCountLevel.SubscribeToText(shootCountLevel).AddTo(this);
            App.Cache.Kernel.PlayerProfile.Upgrades.RP_ShootCountLevel
                                .Subscribe(level => UpdateUI(UpgradeTree.ShootCountPath.UpgradeCost(level), shootCountCost, shootCountUp))
                                .AddTo(this);

            // Stat Reset
            statResetButton.OnClickAsObservable().Subscribe(_ => panelConfirmReset.SetActive(true)).AddTo(this);            
        }


        private void OnUpgradeButtonClick(NotifyEventHandler toUpgrade)
        {
            App.Cache.Kernel.AudioService.PlaySFX(sfxUpgradeClick);
            toUpgrade();
        }

        private void OnCurrencyChange(int currency)
        {
            txtMoney.text = currency.ToString();

        }

        private void UpdateUI(int cost, Text label, Button buy)
        {
            // Zero means MAX level is reached
            if(cost <= 0)
            {
                label.text = "MAX";
                buy.interactable = false;
                return;
            }

            label.text = cost.ToString();

            ReactiveCommand hasEnoughCurrency = App.Cache.Kernel.PlayerProfile.Stats.RP_Currency.Select(x => x >= cost).ToReactiveCommand();
            hasEnoughCurrency.BindTo(buy).AddTo(this);
        }
    }
}
