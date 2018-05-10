using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Source.Service.Audio
{
    public class AudioChannel
    {
        private readonly AudioSourceFactory audioSourceFactory;
        private List<AudioSource> sources = new List<AudioSource>();

        private readonly float minPitch = 0.65f;
        private readonly float maxPitch = 1.5f;

        private float defaultVolume = 0.8f;
        private FloatReactiveProperty volumeProperty;

        public float Volume
        {
            get { return volumeProperty.Value; }
            set { volumeProperty.Value = Mathf.Clamp01(value); }
        }

        public bool IsMuted
        {
            get { return Volume <= 0; }
            set { Volume = value ? 0 : defaultVolume; }
        }


        public AudioChannel(AudioSourceFactory audioSourceFactory, float defaultVolume, bool isMuted)
        {
            this.audioSourceFactory = audioSourceFactory;
            
            // Set Volume
            this.defaultVolume = defaultVolume;
            volumeProperty = new FloatReactiveProperty(defaultVolume);

            // Set volume based on muted flag
            Volume = isMuted ? 0 : volumeProperty.Value;

            // Listen to volume changes
            volumeProperty.Subscribe(_ => UpdateAudioSourceVolumes());
        }


        /* --------------------------------------------------------------------------------------- */
        #region VOLUME MANAGEMENT

        // Updates the Volume of all AudioSources with the currently set one
        private void UpdateAudioSourceVolumes()
        {
            foreach (AudioSource audioSource in sources)
            {
                audioSource.volume = Volume;
            }
        }


        #endregion


        /* --------------------------------------------------------------------------------------- */
        #region PLAY AUDIO

        public void PlayClip(AudioClip clip, bool loop = false, bool randomizePitch = false)
        {
            AudioSource audioSource = GetAudioSource();

            audioSource.clip = clip;
            audioSource.loop = loop;

            if (randomizePitch)
            {
                audioSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            }

            audioSource.Play();
        }


        private AudioSource GetAudioSource()
        {
            AudioSource audioSource = sources.FirstOrDefault(e => !e.isPlaying);
            if (audioSource == null)
            {
                sources.Add(audioSourceFactory.Create());
                audioSource = sources.Last();
                audioSource.volume = Volume;
            }

            return audioSource;
        }

        #endregion
    }
}
