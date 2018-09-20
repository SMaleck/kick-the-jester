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
            Container.Bind<UserControlService>().AsSingle();

            Container.Bind<PlayerModel>().AsSingle().NonLazy();
            Container.Bind<FlightStatsModel>().AsSingle().NonLazy();

            Container.Bind<MotionBoot>().AsSingle().NonLazy();
        }
    }
}
