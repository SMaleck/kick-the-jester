using Assets.Source.App.Initialization;
using Assets.Source.App.Installers.FeatureInstallers;
using Assets.Source.Entities;
using Assets.Source.Entities.Jester.Components;
using Assets.Source.Features.Cheats;
using Assets.Source.Features.GameState;
using Assets.Source.Features.PickupItems;
using Assets.Source.Features.PlayerData;
using Assets.Source.Features.Statistics;
using Assets.Source.Features.Upgrades;
using Assets.Source.Mvc.Controllers;
using Assets.Source.Mvc.Mediation;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.ServiceControllers;
using Assets.Source.Mvc.Views;
using Assets.Source.Util;
using UnityEngine;

namespace Assets.Source.App.Installers.SceneInstallers
{
    // ToDo [ARCH] Installers are not utilized correctly. Could be split up
    public class GameSceneInstaller : AbstractSceneInstaller
    {
        [SerializeField] public BestDistanceMarkerView BestDistanceMarkerView;
        [SerializeField] public HudView HudView;
        [SerializeField] public PauseView PauseView;
        [SerializeField] public ResetProfileConfirmationView ResetProfileConfirmationView;
        [SerializeField] public RoundEndView RoundEndView;
        [SerializeField] public SettingsView SettingsView;
        [SerializeField] public UpgradeScreenView UpgradeScreenView;
        [SerializeField] public CheatView CheatView;
        [SerializeField] public SpawnAnchorEntity SpawnAnchor;
        [SerializeField] public AchievementsScreenView AchievementsScreenView;
        [SerializeField] public AchievementNotificationView _achievementNotificationView;

        protected override void InstallSceneBindings()
        {
            #region MVC

            Container.BindInterfacesAndSelfTo<ClosableViewMediator>().AsSingle().NonLazy();

            Container.BindInstance(HudView).AsSingle();
            Container.BindInterfacesAndSelfTo<HudController>().AsSingle().NonLazy();

            Container.BindInstance(RoundEndView).AsSingle();
            Container.BindInterfacesAndSelfTo<RoundEndController>().AsSingle().NonLazy();

            Container.BindInstance(PauseView).AsSingle();
            Container.BindInterfacesAndSelfTo<PauseController>().AsSingle().NonLazy();

            Container.BindInstance(SettingsView).AsSingle();
            Container.BindInterfacesAndSelfTo<SettingsController>().AsSingle().NonLazy();

            Container.BindInstance(BestDistanceMarkerView).AsSingle();
            Container.BindInterfacesAndSelfTo<BestDistanceMarkerController>().AsSingle().NonLazy();

            Container.BindInstance(ResetProfileConfirmationView).AsSingle();
            Container.BindInterfacesAndSelfTo<ResetProfileConfirmationController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<AudioSettingsController>().AsSingle().NonLazy();

            #endregion

            #region MODELS

            Container.BindInterfacesAndSelfTo<SettingsModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UserInputModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateModel>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<StatisticsModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StatisticsController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StatisticsCollectionController>().AsSingle().NonLazy();

            #endregion

            #region GAME ROUND

            Container.BindInterfacesAndSelfTo<GameStateController>().AsSingle().NonLazy();

            #endregion

            #region PLAYER STATE & UPGRADES

            Container.BindInterfacesAndSelfTo<UpgradeController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAttributesModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAttributesController>().AsSingle().NonLazy();

            Container.BindInstance(UpgradeScreenView).AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeScreenController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<FlightStatsController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FlightStatsModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameRoundStatsModel>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<PlayerProfileController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerProfileModel>().AsSingle().NonLazy();

            #endregion

            #region ITEM SPAWNING

            Container.BindInstance(SpawnAnchor);
            Container.BindInterfacesAndSelfTo<PickupItemSpawner>().AsSingleNonLazy();

            #endregion

            #region JESTER COMPONENTS

            // ToDo [ARCH] This composition could be done in a cleaner way
            Container.BindInterfacesAndSelfTo<FlightRecorder>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MotionBoot>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MotionShoot>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<VelocityLimiter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpriteEffect>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SoundEffect>().AsSingle().NonLazy();

            #endregion

            #region ACHIEVEMENTS

            Container.BindInstance(AchievementsScreenView);
            Container.BindInstance(_achievementNotificationView);
            AchievementsInstaller.Install(Container);

            #endregion

            #region CHEATS

            Container.BindInstance(CheatView).AsSingle();
            Container.BindInterfacesAndSelfTo<CheatController>().AsSingle().NonLazy();

            #endregion

            Container.BindExecutionOrder<GameSceneInitializer>(998);
            Container.BindInterfacesAndSelfTo<GameSceneInitializer>().AsSingle().NonLazy();
        }
    }
}
