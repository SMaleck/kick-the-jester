using Assets.Source.Features.Upgrades;
using Assets.Source.Mvc.Controllers;
using Assets.Source.Mvc.ServiceControllers;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Services.Audio;
using Assets.Source.Services.Particles;
using Assets.Source.Services.Savegame;
using Assets.Source.Services.Upgrade;
using Assets.Source.Util;
using UnityEngine;
using Zenject;

namespace Assets.Source.App.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] public ScreenFadeView ScreenFadeViewPrefab;

        public override void InstallBindings()
        {
            Container.Bind<SavegameService>().AsSingle();
            Container.Bind<UpgradeService>().AsSingle();
            Container.Bind<SceneTransitionService>().AsSingle();
            Container.Bind<AudioService>().AsSingle().NonLazy();
            Container.Bind<ParticleService>().AsSingle().NonLazy();
            Container.Bind<ViewAudioEventController>().AsSingle().NonLazy();

            Container.Bind<ScreenFadeView>().FromComponentInNewPrefab(ScreenFadeViewPrefab).AsSingle();
            Container.Bind<ScreenFadeController>().AsSingle().NonLazy();

            Container.BindPrefabFactory<UpgradeItemView, UpgradeItemView.Factory>();
        }
    }
}
