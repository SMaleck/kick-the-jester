using Assets.Source.Features.Upgrades.Data;
using Assets.Source.Services;
using Assets.Source.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Source.Features.Upgrades
{
    public class UpgradeItemView : AbstractDisposable, IInitializable
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, UpgradeItemView> { }

        [Header("Upgrade Item")]
        [SerializeField] private Image _upgradeItemIcon;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        [Header("Buttons")]
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private TextMeshProUGUI _upgradeButtonText;

        public Subject<Unit> _onUpgradeButtonClicked;
        public IObservable<Unit> OnUpgradeButtonClicked => _onUpgradeButtonClicked;

        public void Initialize()
        {
            _onUpgradeButtonClicked = new Subject<Unit>();
        }

        public void SetUpgradePathType(UpgradePathType upgradePathType)
        {
            _titleText.text = TextService.UpgradeItemTitle(upgradePathType);
            _descriptionText.text = TextService.UpgradeItemDescription(upgradePathType);
        }               
    }
}
