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

        [Header("Path Maxed")]
        [SerializeField] private GameObject _regularParent;
        [SerializeField] private GameObject _maxedParent;

        public readonly ReactiveCommand _onUpgradeButtonClicked = new ReactiveCommand();
        public IObservable<Unit> OnUpgradeButtonClicked => _onUpgradeButtonClicked;

        private UpgradeTreeConfig _upgradeTreeConfig;

        [Inject]
        private void Inject(UpgradeTreeConfig upgradeTreeConfig)
        {
            _upgradeTreeConfig = upgradeTreeConfig;
        }

        public override void Setup()
        {
            _onUpgradeButtonClicked.AddTo(Disposer);
            _onUpgradeButtonClicked.BindTo(_upgradeButton).AddTo(Disposer);

            _upgradeButtonText.text = TextService.Max();
        }

        public void SetUpdateUpgradePathType(UpgradePathType upgradePathType)
        {
            _upgradeItemIcon.sprite = _upgradeTreeConfig.GetUpgradeIcon(upgradePathType);
            _titleText.text = TextService.UpgradeItemTitle(upgradePathType);
            _descriptionText.text = TextService.UpgradeItemDescription(upgradePathType);
        }

        public void SetCost(int cost)
        {
            _upgradeButtonText.text = TextService.CurrencyAmount(cost);
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
            _regularParent.SetActive(!isMaxed);
            _maxedParent.SetActive(isMaxed);
        }
    }
}
