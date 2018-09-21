using UnityEngine;

namespace Assets.Source.App.Audio
{
    public class AudioSourceFactory
    {
        private GameObject audioSourceContainer;


        public AudioSource Create(float volume)
        {
            if(audioSourceContainer == null)
            {
                audioSourceContainer = new GameObject();
                Object.DontDestroyOnLoad(audioSourceContainer);
            }

            AudioSource audioSource = audioSourceContainer.AddComponent<AudioSource>();
            audioSource.volume = volume;
            return audioSource;
        }
    }
}
