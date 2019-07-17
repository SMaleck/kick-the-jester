using Assets.Source.Mvc.Controllers;
using Assets.Source.Mvc.ServiceControllers;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Services.Audio;
using Assets.Source.Services.Particles;
using Assets.Source.Services.Savegame;
using Assets.Source.Services.Upgrade;
using UnityEngine;
using Zenject;

namespace Assets.Source.App.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] public ScreenFadeView ScreenFadeViewPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SavegameService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneTransitionService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AudioService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ParticleService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ViewAudioEventController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<ScreenFadeView>().FromComponentInNewPrefab(ScreenFadeViewPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<ScreenFadeController>().AsSingle().NonLazy();
        }
    }
}
