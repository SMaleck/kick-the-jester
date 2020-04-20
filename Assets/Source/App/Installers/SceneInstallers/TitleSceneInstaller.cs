using Assets.Source.App.Initialization;
using Assets.Source.Features.PlayerData;
using Assets.Source.Mvc.Controllers;
using Assets.Source.Mvc.Mediation;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.ServiceControllers;
using Assets.Source.Mvc.Views;
using UnityEngine;

namespace Assets.Source.App.Installers.SceneInstallers
{
    public class TitleSceneInstaller : AbstractSceneInstaller
    {
        [SerializeField] public TitleView TitleView;
        [SerializeField] public SettingsView SettingsView;
        [SerializeField] public CreditsView CreditsView;
        [SerializeField] public TutorialView TutorialView;
        [SerializeField] public ResetProfileConfirmationView ShopConfirmResetView;

        protected override void InstallSceneBindings()
        {
            Container.BindInterfacesAndSelfTo<ClosableViewMediator>().AsSingle().NonLazy();

            Container.BindInstance(TitleView).AsSingle();
            Container.BindInterfacesAndSelfTo<TitleController>().AsSingle().NonLazy();

            Container.BindInstance(SettingsView).AsSingle();
            Container.BindInterfacesAndSelfTo<SettingsController>().AsSingle().NonLazy();

            Container.BindInstance(CreditsView).AsSingle();

            Container.BindInstance(TutorialView).AsSingle();
            Container.BindInterfacesAndSelfTo<TutorialController>().AsSingle().NonLazy();

            Container.BindInstance(ShopConfirmResetView).AsSingle();
            Container.BindInterfacesAndSelfTo<ResetProfileConfirmationController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<AudioSettingsController>().AsSingle().NonLazy();

            #region MODELS

            Container.BindInterfacesAndSelfTo<SettingsModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerProfileModel>().AsSingle().NonLazy();

            #endregion

            Container.BindExecutionOrder<TitleSceneInitializer>(998);
            Container.BindInterfacesAndSelfTo<TitleSceneInitializer>().AsSingle().NonLazy();
        }
    }
}