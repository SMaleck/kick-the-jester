using Assets.Source.Features.Upgrades;
using Assets.Source.Features.Upgrades.Data;
using Assets.Source.Mvc.Data;
using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using Assets.Source.Util;
using System.Linq;
using Assets.Source.Features.PlayerData;
using Assets.Source.Services;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class UpgradeScreenController : ClosableController
    {
        private readonly UpgradeScreenView _upgradeScreenView;
        private readonly ViewPrefabConfig _viewPrefabConfig;
        private readonly UpgradeItemView.Factory _upgradeItemViewFactory;
        private readonly UpgradeController _upgradeController;

        public UpgradeScreenController(
            UpgradeScreenView view,
            OpenPanelModel openPanelModel,
            ViewPrefabConfig viewPrefabConfig,
            UpgradeItemView.Factory upgradeItemViewFactory,
            UpgradeController upgradeController,
            PlayerProfileModel playerProfileModel,
            SceneTransitionService sceneTransitionService)
            : base(view)
        {
            _upgradeScreenView = view;
            _viewPrefabConfig = viewPrefabConfig;
            _upgradeItemViewFactory = upgradeItemViewFactory;
            _upgradeController = upgradeController;

            view.Initialize();

            EnumHelper<UpgradePathType>.Iterator
                .ToList()
                .ForEach(CreateUpgradeItemView);

            openPanelModel.OnOpenUpgrades
                .Subscribe(_ => Open())
                .AddTo(Disposer);

            view.OnPlayAgainClicked
                .Subscribe(_ => sceneTransitionService.ToGame())
                .AddTo(Disposer);

            view.OnResetClicked
                .Subscribe(_ => openPanelModel.OpenResetConfirmation())
                .AddTo(Disposer);

            playerProfileModel.CurrencyAmount
                .Subscribe(view.SetCurrencyAmount)
                .AddTo(Disposer);
        }

        private void CreateUpgradeItemView(UpgradePathType upgradePathType)
        {
            var upgradeItemView = _upgradeItemViewFactory.Create(_viewPrefabConfig.UpgradeItemViewPrefab);

            upgradeItemView.transform.SetParent(
                _upgradeScreenView.UpgradeItemsLayoutParent,
                false);

            upgradeItemView.SetUpdateUpgradePathType(upgradePathType);
            upgradeItemView.Initialize();

            upgradeItemView.OnUpgradeButtonClicked
                .Subscribe(_ => _upgradeController.TryUpgrade(upgradePathType))
                .AddTo(Disposer);

            var upgradeModel = _upgradeController.GetUpgradeModel(upgradePathType);

            upgradeModel.Level
                .Subscribe(upgradeItemView.SetLevel)
                .AddTo(Disposer);

            upgradeModel.IsMaxed
                .Subscribe(upgradeItemView.SetIsMaxed)
                .AddTo(Disposer);

            upgradeModel.Cost
                .Subscribe(upgradeItemView.SetCost)
                .AddTo(Disposer);

            upgradeModel.CanAfford
                .Subscribe(upgradeItemView.SetCanAfford)
                .AddTo(Disposer);
        }
    }
}
