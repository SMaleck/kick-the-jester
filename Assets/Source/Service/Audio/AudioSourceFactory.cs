using UnityEngine;

namespace Assets.Source.Service.Audio
{
    public class AudioSourceFactory
    {
        private GameObject audioSourceContainer;


        public AudioSource Create()
        {
            if(audioSourceContainer == null)
            {
                audioSourceContainer = new GameObject();
                Object.DontDestroyOnLoad(audioSourceContainer);
            }

            AudioSource audioSource = audioSourceContainer.AddComponent<AudioSource>();
            return audioSource;
        }
    }
}
