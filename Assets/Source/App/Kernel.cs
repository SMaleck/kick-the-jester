using Assets.Source.App.Audio;
using Assets.Source.App.ParticleEffects;
using Assets.Source.App.Storage;
using Assets.Source.App.Upgrade;
using UniRx;
using UnityEngine;

namespace Assets.Source.App
{
    /// <summary>
    /// The Kernel initialises all artefacts which do NOT destroy between scene loads.
    /// This is added to the title scene
    /// </summary>
    public class Kernel : MonoBehaviour
    {
        public static BoolReactiveProperty Ready = new BoolReactiveProperty(false);        

        public static AppState AppState { get; private set; }
        public static SceneTransitionService SceneTransitionService { get; private set; }
        public static AudioService AudioService { get; private set; }
        public static PlayerProfileService PlayerProfileService { get; private set; }
        public static UserSettingsService UserSettingsService { get; private set; }
        public static UpgradeService UpgradeService { get; private set; }
        public static PfxService PfxService { get; private set; }


        private void Awake()
        {
            Kernel.Ready.Value = false;

            DontDestroyOnLoad(gameObject);

            // Initialisation order is important due to inter-dependencies
            // TODO: Resolve implicit interdependency
            AppState = new AppState();
            SceneTransitionService = new SceneTransitionService();
            PlayerProfileService = new PlayerProfileService(new FileDataStorage<PlayerProfile>("profile.save"));
            UserSettingsService = new UserSettingsService();
            AudioService = new AudioService();
            UpgradeService = new UpgradeService(PlayerProfileService);
            PfxService = new PfxService();

            Kernel.Ready.Value = true;
        }
    }
}
