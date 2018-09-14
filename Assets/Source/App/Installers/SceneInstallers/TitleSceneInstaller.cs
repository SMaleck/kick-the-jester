using Assets.Source.Mvc.Controllers;
using Assets.Source.Mvc.Views;
using UnityEngine;
using Zenject;

namespace Assets.Source.App.Installers.SceneInstallers
{
    public class TitleSceneInstaller : MonoInstaller
    {        
        [SerializeField] public TitleView TitleView;
        [SerializeField] public SettingsView SettingsView;
        [SerializeField] public CreditsView CreditsView;

        public override void InstallBindings()
        {
            Container.BindInstance(TitleView).AsSingle();
            Container.Bind<TitleController>().AsSingle().NonLazy();

            Container.BindInstance(SettingsView).AsSingle();
            Container.Bind<SettingsController>().AsSingle().NonLazy();

            Container.BindInstance(CreditsView).AsSingle();
            Container.Bind<CreditsController>().AsSingle().NonLazy();
        }
    }
}