using Assets.Source.Services;
using Assets.Source.Services.Audio;
using Assets.Source.Services.Particles;
using Assets.Source.Services.Savegame;
using Zenject;

namespace Assets.Source.App.Installers
{
    public class ServiceInstaller : Installer<ServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SettingsService>().AsSingle();
            Container.Bind<SavegameService>().AsSingle();
            Container.Bind<Assets.Source.Services.SceneTransitionService>().AsSingle();
            Container.Bind<AudioService>().AsSingle();
            Container.Bind<ParticleService>().AsSingle();
        }
    }
}
