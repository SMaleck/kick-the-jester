using Assets.Source.Features.Upgrades.Data;
using Assets.Source.Services;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Source.Mvc.Views
{
    public class UpgradeItemView : AbstractView
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, UpgradeItemView> { }

        [Header("Upgrade Item")]
        [SerializeField] private Image _upgradeItemIcon;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _levelText;

        [Header("Buttons")]
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private TextMeshProUGUI _upgradeButtonText;

        public Subject<Unit> _onUpgradeButtonClicked;
        public IObservable<Unit> OnUpgradeButtonClicked => _onUpgradeButtonClicked;

        private UpgradePathType _upgradePathType;
        private bool _isMaxed;

        public override void Setup()
        {
            _onUpgradeButtonClicked = new Subject<Unit>();

            _upgradeButton.OnClickAsObservable()
                .Subscribe(_ => _onUpgradeButtonClicked.OnNext(Unit.Default))
                .AddTo(Disposer);
        }

        public void SetUpdateUpgradePathType(UpgradePathType upgradePathType)
        {
            _upgradePathType = upgradePathType;

            _titleText.text = TextService.UpgradeItemTitle(upgradePathType);
            _descriptionText.text = TextService.UpgradeItemDescription(upgradePathType);
        }

        public void SetCost(int cost)
        {
            // 0 means MaxLevel was reached
            _upgradeButtonText.text = _isMaxed
                ? TextService.Max()
                : TextService.CurrencyAmount(cost);
        }

        public void SetLevel(int level)
        {
            _levelText.text = TextService.LevelX(level);
        }

        public void SetCanAfford(bool canAfford)
        {
            _upgradeButton.interactable = canAfford;
        }

        public void SetIsMaxed(bool isMaxed)
        {
            _isMaxed = isMaxed;
            if (_isMaxed)
            {
                _upgradeButtonText.text = TextService.Max();
            }
        }
    }
}
