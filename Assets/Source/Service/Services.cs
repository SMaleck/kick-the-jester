using Assets.Source.Behaviours.MainCamera;
using Assets.Source.Service.Audio;
using UnityEngine;

namespace Assets.Source.Service
{
    public class Services : MonoBehaviour
    {
        // Keep Alive across Scene loads
        private void Awake()
        {
            DontDestroyOnLoad(gameObject); 
        }


        private AudioService audioService;
        public AudioService AudioService
        {
            get
            {
                if(audioService == null)
                {
                    audioService = new AudioService();
                }

                return audioService;
            }
        }


        private SceneTransitionService sceneTransitionService;
        public SceneTransitionService SceneTransitionService
        {
            get
            {
                if (sceneTransitionService == null)
                {
                    sceneTransitionService = new SceneTransitionService();
                }

                return sceneTransitionService;
            }
        }
    }
}
