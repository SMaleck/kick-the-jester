using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    // ToDo See Github Issue #92
    // ToDo [IMPORTANT] Buy buttons stay not interactable
    public class ShopView : ClosableView
    {
        [Header("General")]
        [SerializeField] private Button _statResetButton;
        [SerializeField] private Text _txtMoney;        


        [Header("Upgrades")]

        // Max Velocity
        [SerializeField] private Text _maxVelocityLevel;
        [SerializeField] private Text _maxVelocityCost;
        [SerializeField] private Button _maxVelocityUp;

        // Kick Force
        [SerializeField] private Text _kickForceLevel;
        [SerializeField] private Text _kickForceCost;
        [SerializeField] private Button _kickForceUp;

        // Shoot Force
        [SerializeField] private Text _shootForceLevel;
        [SerializeField] private Text _shootForceCost;
        [SerializeField] private Button _shootForceUp;

        // Shoot Force
        [SerializeField] private Text _shootCountLevel;
        [SerializeField] private Text _shootCountCost;
        [SerializeField] private Button _shootCountUp;


        public int Currency { set { _txtMoney.text = value.ToString(); } }

        public int MaxVelocityLevel { set { _maxVelocityLevel.text = value.ToString(); } }
        public int MaxVelocityCost { set { UpdateCostFor(value, _maxVelocityCost, _maxVelocityUp); } }
        public bool MaxVelocityCanAfford { set { _maxVelocityUp.interactable = value; } }
        public ReactiveCommand OnMaxVelocityLevelUp = new ReactiveCommand();

        public int KickForceLevel { set { _kickForceLevel.text = value.ToString(); } }
        public int KickForceCost { set { UpdateCostFor(value, _kickForceCost, _kickForceUp); } }
        public bool KickForceCanAfford { set { _kickForceUp.interactable = value; } }
        public ReactiveCommand OnKickForceLevelUp = new ReactiveCommand();

        public int ShootForceLevel { set { _shootForceLevel.text = value.ToString(); } }
        public int ShootForceCost { set { UpdateCostFor(value, _shootForceCost, _shootForceUp); } }
        public bool ShootForceCanAfford { set { _shootForceUp.interactable = value; } }
        public ReactiveCommand OnShootForceLevelUp = new ReactiveCommand();

        public int ShootCountLevel { set { _shootCountLevel.text = value.ToString(); } }
        public int ShootCountCost { set { UpdateCostFor(value, _shootCountCost, _shootCountUp); } }
        public bool ShootCountCanAfford { set { _shootCountUp.interactable = value; } }
        public ReactiveCommand OnShootCountLevelUp = new ReactiveCommand();

        public ReactiveCommand OnResetClicked = new ReactiveCommand();


        public override void Setup()
        {
            base.Setup();

            _maxVelocityUp.OnClickAsObservable()
                .Subscribe(_ => OnMaxVelocityLevelUp.Execute())
                .AddTo(this);

            _kickForceUp.OnClickAsObservable()
                .Subscribe(_ => OnKickForceLevelUp.Execute())
                .AddTo(this);

            _shootForceUp.OnClickAsObservable()
                .Subscribe(_ => OnShootForceLevelUp.Execute())
                .AddTo(this);

            _shootCountUp.OnClickAsObservable()
                .Subscribe(_ => OnShootCountLevelUp.Execute())
                .AddTo(this);

            _statResetButton.OnClickAsObservable()
                .Subscribe(_ => OnResetClicked.Execute())
                .AddTo(this);            
        }


        private void UpdateCostFor(int cost, Text uiText, Button button)
        {
            // Zero means MAX level is reached
            if (cost <= 0)
            {
                uiText.text = "MAX";
                button.interactable = false;
                return;
            }

            uiText.text = cost.ToString();
        }
    }
}
