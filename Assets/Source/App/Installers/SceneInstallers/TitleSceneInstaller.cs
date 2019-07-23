using Assets.Source.Mvc.Controllers;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.ServiceControllers;
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
        [SerializeField] public TutorialView TutorialView;
        [SerializeField] public ResetProfileConfirmationView ShopConfirmResetView;

        public override void InstallBindings()
        {
            Container.BindInstance(TitleView).AsSingle();
            Container.BindInterfacesAndSelfTo<TitleController>().AsSingle().NonLazy();

            Container.BindInstance(SettingsView).AsSingle();
            Container.BindInterfacesAndSelfTo<SettingsController>().AsSingle().NonLazy();

            Container.BindInstance(CreditsView).AsSingle();
            Container.BindInterfacesAndSelfTo<CreditsController>().AsSingle().NonLazy();

            Container.BindInstance(TutorialView).AsSingle();
            Container.BindInterfacesAndSelfTo<TutorialController>().AsSingle().NonLazy();

            Container.BindInstance(ShopConfirmResetView).AsSingle();
            Container.BindInterfacesAndSelfTo<ResetProfileConfirmationController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<AudioSettingsController>().AsSingle().NonLazy();

            #region MODELS

            Container.BindInterfacesAndSelfTo<TitleModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SettingsModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<OpenPanelModel>().AsSingle().NonLazy();

            #endregion

            Container.BindExecutionOrder<SceneStartController>(999);
            Container.BindInterfacesAndSelfTo<SceneStartController>().AsSingle().NonLazy();
        }
    }
}