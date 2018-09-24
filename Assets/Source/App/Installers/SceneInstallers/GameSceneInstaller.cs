using Assets.Source.Entities.Jester.Components;
using Assets.Source.Mvc.Models;
using Assets.Source.Services;
using Zenject;

namespace Assets.Source.App.Installers.SceneInstallers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            #region SERVICES

            Container.Bind<UserControlService>().AsSingle();

            #endregion


            #region MVC



            #endregion


            #region MODELS

            Container.Bind<PlayerModel>().AsSingle().NonLazy();
            Container.Bind<FlightStatsModel>().AsSingle().NonLazy();

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
