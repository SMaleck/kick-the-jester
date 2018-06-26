using Assets.Source.App.Audio;
using Assets.Source.App.ParticleEffects;
using Assets.Source.App.Persistence;
using Assets.Source.App.Persistence.Storage;
using Assets.Source.App.Upgrade;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Source.App
{
    /// <summary>
    /// The Kernel initialises all artefacts which do NOT destroy between scene loads.
    /// This is added to the title scene
    /// </summary>
    public class Kernel : MonoInstaller<AppInstaller>
    {
        public BoolReactiveProperty Ready = new BoolReactiveProperty(false);        

        public AppState AppState { get { return Find<AppState>(); } }
        public SceneTransitionService SceneTransitionService { get { return Find<SceneTransitionService>(); } }
        public AudioService AudioService { get { return Find<AudioService>(); } }        
        public PlayerProfileContext PlayerProfile { get { return Find<PlayerProfileContext>(); } }
        public UserSettingsContext UserSettings { get { return Find<UserSettingsContext>(); } }
        public UpgradeService UpgradeService { get { return Find<UpgradeService>(); } }
        public PfxService PfxService { get { return Find<PfxService>(); } }

        private T Find<T>()
        {
            return Container.Resolve<T>();
        }

        public DiContainer MainContainer { get { return Container; } }

        private void Awake()
        {
            //App.Cache.Kernel.Ready.Value = false;

            DontDestroyOnLoad(gameObject);
            
            //App.Cache.Kernel.Ready.Value = true;
        }

        public override void InstallBindings()
        {
            Container.BindInstance("MyTest").WhenInjectedInto<Test>();
            Container.Bind<Test>().AsSingle().NonLazy();

            Container.Bind<AppState>().AsSingle();
            Container.Bind<SceneTransitionService>().AsSingle().NonLazy();

            JsonStorage profileStorage = new JsonStorage("profile.save");
            Container.BindInstance(profileStorage).WhenInjectedInto<PlayerProfileContext>();
            Container.Bind<PlayerProfileContext>().AsSingle();

            Container.Bind<UserSettingsContext>().AsSingle();
            Container.Bind<AudioService>().AsSingle().NonLazy();
            Container.Bind<UpgradeService>().AsSingle().NonLazy();
            Container.Bind<PfxService>().AsSingle().NonLazy();

            Ready.Value = true;
        }
    }
}
