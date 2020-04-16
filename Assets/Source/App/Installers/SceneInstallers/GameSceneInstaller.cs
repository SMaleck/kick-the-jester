using Assets.Source.App.Initialization;
using Assets.Source.Entities.Items;
using Assets.Source.Entities.Jester.Components;
using Assets.Source.Features.Achievements;
using Assets.Source.Features.Achievements.RequirementStrategies;
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
using Assets.Source.Mvc.Views.PartialViews;
using Assets.Source.Services.Savegames.Models;
using Assets.Source.Util;
using System.Collections.Generic;
using Assets.Source.Entities;
using UnityEngine;

namespace Assets.Source.App.Installers.SceneInstallers
{
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

        protected override void InstallSceneBindings()
        {
            #region MVC

            Container.BindInterfacesAndSelfTo<ClosableViewMediator>().AsSingle().NonLazy();
            Container.BindFactory<IClosableView, ClosableViewController, ClosableViewController.Factory>();

            Container.BindInstance(HudView).AsSingle();
            Container.BindPrefabFactory<PickupFeedbackView, PickupFeedbackView.Factory>();
            Container.BindInterfacesAndSelfTo<HudController>().AsSingle().NonLazy();

            Container.BindPrefabFactory<CurrencyGainItemView, CurrencyGainItemView.Factory>();
            Container.BindInstance(RoundEndView).AsSingle();
            Container.BindInterfacesAndSelfTo<RoundEndController>().AsSingle().NonLazy();

            Container.BindInstance(PauseView).AsSingle();
            Container.BindInterfacesAndSelfTo<PauseController>().AsSingle().NonLazy();

            Container.BindInstance(SettingsView).AsSingle();
            Container.BindInterfacesAndSelfTo<SettingsController>().AsSingle().NonLazy();

            Container.BindInstance(BestDistanceMarkerView).AsSingle();

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

            Container.BindPrefabFactory<UpgradeItemView, UpgradeItemView.Factory>();

            Container.BindInterfacesAndSelfTo<FlightStatsController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FlightStatsModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameRoundStatsModel>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<PlayerProfileController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerProfileModel>().AsSingle().NonLazy();

            #endregion

            #region ITEM SPAWNING

            Container.BindPrefabFactory<AbstractItemEntity, AbstractItemEntity.Factory>();
            Container.BindInstance(SpawnAnchor);
            Container.BindInterfacesAndSelfTo<PickupItemSpawner>().AsSingleNonLazy();

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

            #region ACHIEVEMENTS

            Container.BindFactory<AchievementSavegame, AchievementModel, AchievementModel.Factory>().AsSingle();
            Container.BindFactory<List<IRequirementStrategy>, AchievementUnlockController, AchievementUnlockController.Factory>().AsSingle();

            Container.BindInterfacesAndSelfTo<RequirementStrategyFactory>().AsSingle();
            Container.BindFactory<AchievementModel, ReachedMoonRequirementStrategy, ReachedMoonRequirementStrategy.Factory>().AsSingle();
            Container.BindFactory<AchievementModel, TotalDistanceRequirementStrategy, TotalDistanceRequirementStrategy.Factory>().AsSingle();
            Container.BindFactory<AchievementModel, BestDistanceRequirementStrategy, BestDistanceRequirementStrategy.Factory>().AsSingle();
            Container.BindFactory<AchievementModel, BestHeightRequirementStrategy, BestHeightRequirementStrategy.Factory>().AsSingle();
            Container.BindFactory<AchievementModel, RoundsPlayedRequirementStrategy, RoundsPlayedRequirementStrategy.Factory>().AsSingle();

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
