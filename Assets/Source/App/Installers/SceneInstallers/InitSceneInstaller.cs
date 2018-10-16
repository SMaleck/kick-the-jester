using Assets.Source.Mvc.ServiceControllers;
using Zenject;

namespace Assets.Source.App.Installers.SceneInstallers
{
    public class InitSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AppStartController>().AsSingle().NonLazy();
        }
    }
}
