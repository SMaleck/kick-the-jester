using Assets.Source.App;
using Assets.Source.App.Upgrade;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class ShopPanel : AbstractPanel
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


        public override void Setup()
        {
            base.Setup();

            backButton.OnClickAsObservable().Subscribe(_ => Kernel.SceneTransitionService.ToGame());

            Kernel.PlayerProfileService.RP_Currency
                                       .Subscribe(OnCurrencyChange)                                       
                                       .AddTo(this);

            // Upgrades
            maxVelocityUp.OnClickAsObservable().Subscribe(_ => Kernel.UpgradeService.MaxVelocityUp()).AddTo(this);
            Kernel.PlayerProfileService.RP_MaxVelocityLevel.SubscribeToText(maxVelocityLevel).AddTo(this);
            Kernel.PlayerProfileService.RP_MaxVelocityLevel
                .Subscribe(level => UpdateUI(UpgradeTree.MaxVelocityPath.UpgradeCost(level), maxVelocityCost, maxVelocityUp))
                .AddTo(this);
            
            kickForceUp.OnClickAsObservable().Subscribe(_ => Kernel.UpgradeService.KickForceUp()).AddTo(this);
            Kernel.PlayerProfileService.RP_KickForceLevel.SubscribeToText(kickForceLevel).AddTo(this);
            Kernel.PlayerProfileService.RP_KickForceLevel
                .Subscribe(level => UpdateUI(UpgradeTree.KickForcePath.UpgradeCost(level), kickForceCost, kickForceUp))
                .AddTo(this);

            shootForceUp.OnClickAsObservable().Subscribe(_ => Kernel.UpgradeService.ShootForceUp()).AddTo(this);
            Kernel.PlayerProfileService.RP_ShootForceLevel.SubscribeToText(shootForceLevel).AddTo(this);
            Kernel.PlayerProfileService.RP_ShootForceLevel
                .Subscribe(level => UpdateUI(UpgradeTree.ShootForcePath.UpgradeCost(level), shootForceCost, shootForceUp))
                .AddTo(this);

            shootCountUp.OnClickAsObservable().Subscribe(_ => Kernel.UpgradeService.ShootCountUp()).AddTo(this);
            Kernel.PlayerProfileService.RP_ShootCountLevel.SubscribeToText(shootCountLevel).AddTo(this);
            Kernel.PlayerProfileService.RP_ShootCountLevel
                .Subscribe(level => UpdateUI(UpgradeTree.ShootCountPath.UpgradeCost(level), shootCountCost, shootCountUp))
                .AddTo(this);

            // Stat Reset
            statResetButton.OnClickAsObservable().Subscribe(_ => Kernel.PlayerProfileService.ResetStats()).AddTo(this);
        }


        private void OnCurrencyChange(int currency)
        {
            txtMoney.text = currency.ToString();

        }


        private void CheckUpdatesAffordable()
        {

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

            ReactiveCommand hasEnoughCurrency = Kernel.PlayerProfileService.RP_Currency.Select(x => x >= cost).ToReactiveCommand();
            hasEnoughCurrency.BindTo(buy).AddTo(this);
        }
    }
}
