﻿using Assets.Source.Mvc.Controllers;
using Assets.Source.Mvc.ServiceControllers;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Services.Audio;
using Assets.Source.Services.Particles;
using Assets.Source.Services.Savegames;
using Assets.Source.Util;
using Assets.Source.Util.DataStorageStrategies;
using UnityEngine;
using Zenject;

namespace Assets.Source.App.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] public ScreenFadeView ScreenFadeViewPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<JsonDataStorageStrategy>().AsSingle();
            Container.BindInterfacesAndSelfTo<SavegameService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SavegamePersistenceScheduler>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneTransitionService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ViewAudioEventController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<ScreenFadeView>().FromComponentInNewPrefab(ScreenFadeViewPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<ScreenFadeController>().AsSingle().NonLazy();

            Container.BindPrefabFactory<ParticlePoolItem, ParticlePoolItem.Factory>();
            Container.BindInterfacesAndSelfTo<ParticleService>().AsSingle().NonLazy();

            Container.BindPrefabFactory<AudioPoolItem, AudioPoolItem.Factory>();
            Container.BindInterfacesAndSelfTo<AudioService>().AsSingle().NonLazy();
        }
    }
}
