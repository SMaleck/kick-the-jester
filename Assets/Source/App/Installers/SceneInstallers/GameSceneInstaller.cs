using Assets.Source.Entities.GameRound.Components;
using Assets.Source.Entities.Jester.Components;
using Assets.Source.Mvc.Controllers;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.ServiceControllers;
using Assets.Source.Mvc.Views;
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
        [SerializeField] public ShopView ShopView;
        [SerializeField] public ShopConfirmResetView ShopConfirmResetView;        

        public override void InstallBindings()
        {
            #region MVC

            Container.BindInstance(HudView).AsSingle();
            Container.Bind<HudController>().AsSingle().NonLazy();

            Container.BindInstance(RoundEndView).AsSingle();
            Container.Bind<RoundEndController>().AsSingle().NonLazy();

            Container.BindInstance(PauseView).AsSingle();
            Container.Bind<PauseController>().AsSingle().NonLazy();

            Container.BindInstance(BestDistanceMarkerView).AsSingle();
            Container.Bind<BestDistanceMarkerController>().AsSingle().NonLazy();

            Container.BindInstance(ShopView).AsSingle();
            Container.Bind<ShopController>().AsSingle().NonLazy();

            Container.BindInstance(ShopConfirmResetView).AsSingle();
            Container.Bind<ShopConfirmResetController>().AsSingle().NonLazy();

            Container.Bind<AudioSettingsController>().AsSingle().NonLazy();

            #endregion


            #region MODELS

            Container.Bind<ProfileModel>().AsSingle().NonLazy();
            Container.Bind<PlayerModel>().AsSingle().NonLazy();
            Container.Bind<UpgradesModel>().AsSingle().NonLazy();
            Container.Bind<SettingsModel>().AsSingle().NonLazy();
            Container.Bind<UserInputModel>().AsSingle().NonLazy();

            Container.Bind<GameStateModel>().AsSingle().NonLazy();
            Container.Bind<FlightStatsModel>().AsSingle().NonLazy();
            Container.Bind<RoundEndModel>().AsSingle().NonLazy();
            Container.Bind<ShopModel>().AsSingle().NonLazy();

            #endregion


            #region GAME ROUND

            Container.Bind<GameState>().AsSingle().NonLazy();
            Container.Bind<CurrencyRecorder>().AsSingle().NonLazy();
            Container.Bind<RoundStatsRecorder>().AsSingle().NonLazy();

            #endregion


            #region JESTER COMPONENTS

            // ToDo Find better way of composing the Jester
            Container.Bind<FlightRecorder>().AsSingle().NonLazy();
            Container.Bind<MotionBoot>().AsSingle().NonLazy();
            Container.Bind<MotionShoot>().AsSingle().NonLazy();
            Container.Bind<VelocityLimiter>().AsSingle().NonLazy();
            Container.Bind<SpriteEffect>().AsSingle().NonLazy();
            Container.Bind<SoundEffect>().AsSingle().NonLazy();

            #endregion
        }
    }
}
