using Assets.Source.App.Configuration;
using Assets.Source.Mvc;
using Assets.Source.Mvc.Controllers;
using Assets.Source.Mvc.Views;
using Zenject;

namespace Assets.Source.App.Installers
{
    public class MvcInstaller : Installer<MvcInstaller>
    {
        private readonly ViewPrefabConfig _viewPrefabConfig;

        public MvcInstaller(ViewPrefabConfig viewPrefabConfig)
        {
            _viewPrefabConfig = viewPrefabConfig;
        }

        public override void InstallBindings()
        {
            Container.Bind<ScreenFadeView>().FromComponentInNewPrefab(_viewPrefabConfig.ScreenFadeView).AsSingle();
            Container.Bind<ScreenFadeController>().AsSingle();
        }
    }
}
