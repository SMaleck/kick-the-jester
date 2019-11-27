using Assets.Source.Mvc.ServiceControllers;
using Assets.Source.Util;
using UniRx;
using Zenject;

namespace Assets.Source.App.Installers.SceneInstallers
{
    public abstract class AbstractSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CompositeDisposable>()
                .AsSingleNonLazy();

            InstallSceneBindings();
            PostInstall();
        }

        protected abstract void InstallSceneBindings();

        private void PostInstall()
        {
            Container.BindExecutionOrder<SceneStartController>(999);
            Container.BindInterfacesAndSelfTo<SceneStartController>().AsSingle().NonLazy();
        }
    }
}
