using Assets.Source.App;
using Assets.Source.AppKernel.Audio;
using Assets.Source.AppKernel.UserData;
using Assets.Source.Models;
using UniRx;
using UnityEngine;

namespace Assets.Source.AppKernel
{
    /// <summary>
    /// The Kernel initialises all artefacts which do NOT destroy between scene loads.
    /// This is added to the title scene
    /// </summary>
    public class Kernel : MonoBehaviour
    {
        public static BoolReactiveProperty Ready = new BoolReactiveProperty(false);


        public static SceneTransitionService SceneTransitionService { get; private set; }
        public static AudioService AudioService { get; private set; }
        public static PlayerProfileService PlayerProfileService { get; private set; }
        public static UserSettingsService UserSettingsService { get; private set; }


        private void Awake()
        {
            Kernel.Ready.Value = false;

            DontDestroyOnLoad(gameObject);

            // Initialisation order is important due to inter-dependencies
            // TODO: Resolve implicit interdependency
            SceneTransitionService = new SceneTransitionService();
            PlayerProfileService = new PlayerProfileService(new FileDataStorage<PlayerProfile>("profile.save"));
            UserSettingsService = new UserSettingsService();            
            AudioService = new AudioService();

            Kernel.Ready.Value = true;
        }
    }
}
