using Assets.Source.Entities.GameRound.Components;
using Assets.Source.Entities.Jester.Components;
using Assets.Source.Features.Cheats;
using Assets.Source.Features.PlayerData;
using Assets.Source.Features.Upgrades;
using Assets.Source.Mvc.Controllers;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.ServiceControllers;
using Assets.Source.Mvc.Views;
using Assets.Source.Util;
using UnityEngine;
using Zenject;

namespace Assets.Source.App.Installers.SceneInstallers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] public HudView HudView;
        [SerializeField] public RoundEndView RoundEndView;
        [SerializeField] public PauseView PauseView;
        [SerializeField] public BestDistanceMarkerView BestDistanceMarkerView;
        [SerializeField] public ShopConfirmResetView ShopConfirmResetView;
        [SerializeField] public UpgradeScreenView UpgradeScreenView;

        public override void InstallBindings()
        {
            #region MVC

            Container.BindInstance(HudView).AsSingle();
            Container.BindInterfacesAndSelfTo<HudController>().AsSingle().NonLazy();

            Container.BindInstance(RoundEndView).AsSingle();
            Container.BindInterfacesAndSelfTo<RoundEndController>().AsSingle().NonLazy();

            Container.BindInstance(PauseView).AsSingle();
            Container.BindInterfacesAndSelfTo<PauseController>().AsSingle().NonLazy();

            Container.BindInstance(BestDistanceMarkerView).AsSingle();
            Container.BindInterfacesAndSelfTo<BestDistanceMarkerController>().AsSingle().NonLazy();

            Container.BindInstance(ShopConfirmResetView).AsSingle();
            Container.BindInterfacesAndSelfTo<ResetProfileConfirmationController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<AudioSettingsController>().AsSingle().NonLazy();

            #endregion
            
            #region MODELS

            Container.BindInterfacesAndSelfTo<PlayerProfileModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SettingsModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UserInputModel>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<GameStateModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FlightStatsModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<OpenPanelModel>().AsSingle().NonLazy();            

            #endregion
            
            #region GAME ROUND

            Container.BindInterfacesAndSelfTo<GameState>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CurrencyRecorder>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RoundStatsRecorder>().AsSingle().NonLazy();

            #endregion
            
            #region PLAYER STATE & UPGRADES

            Container.BindInterfacesAndSelfTo<UpgradeController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAttributesModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAttributesController>().AsSingle().NonLazy();

            Container.BindInstance(UpgradeScreenView).AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeScreenController>().AsSingle().NonLazy();

            Container.BindPrefabFactory<UpgradeItemView, UpgradeItemView.Factory>();

            #endregion

            #region JESTER COMPONENTS

            // ToDo Find better way of composing the Jester
            Container.BindInterfacesAndSelfTo<FlightRecorder>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MotionBoot>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MotionShoot>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<VelocityLimiter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpriteEffect>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SoundEffect>().AsSingle().NonLazy();

            #endregion
            
            #region CHEATS

            Container.BindInterfacesAndSelfTo<CheatController>().AsSingle().NonLazy();

            #endregion

            Container.BindExecutionOrder<SceneStartController>(999);
            Container.BindInterfacesAndSelfTo<SceneStartController>().AsSingle().NonLazy();
        }
    }
}
