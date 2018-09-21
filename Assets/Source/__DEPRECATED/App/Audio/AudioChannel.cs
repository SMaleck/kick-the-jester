using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Source.App.Audio
{
    public class AudioChannel
    {
        private readonly AudioSourceFactory audioSourceFactory;
        private List<AudioSource> sources = new List<AudioSource>();

        // Limits AudioSources to 1 and forces reuse even if it did not finish playing
        private bool useSingleSource = false;

        // Pitch Limits for randomized sounds
        private readonly float minPitch = 0.65f;
        private readonly float maxPitch = 1.5f;

        // Volume
        private float defaultVolume;
        private float previousVolume;
        public FloatReactiveProperty VolumeProperty = new FloatReactiveProperty(1);
        public float Volume
        {
            get
            {
                return VolumeProperty.Value;
            }
            set
            {
                VolumeProperty.Value = Mathf.Clamp01(value);
                IsMuted = value <= 0;
            }
        }

        // Muted State
        public BoolReactiveProperty IsMutedProperty = new BoolReactiveProperty(false);
        public bool IsMuted
        {
            get
            {
                return IsMutedProperty.Value;
            }
            set
            {                
                if(IsMuted != value)
                {
                    IsMutedProperty.Value = value;
                    UpdateVolumeOnMuted();
                }                               
            }
        }


        public AudioChannel(AudioSourceFactory audioSourceFactory, float defaultVolume, float volume, bool useSingleSource)
        {
            this.audioSourceFactory = audioSourceFactory;

            // set single source operation mode
            this.useSingleSource = useSingleSource;

            // Set default and current Volume
            this.defaultVolume = defaultVolume;                       
            Volume = volume;            

            // Listen to volume changes
            VolumeProperty.Subscribe(_ => UpdateAudioSourceVolumes());            
        }


        /* --------------------------------------------------------------------------------------- */
        #region VOLUME MANAGEMENT

        public void UpdateVolumeOnMuted()
        {
            // If Muted, store the current volume (or default if current is 0)
            // and set Volume to zero
            if (IsMuted && Volume > 0)
            {
                previousVolume = (Volume > 0) ? Volume : defaultVolume;
                Volume = 0;
            }
            // If unmuted, restore previous or default volume
            else if(!IsMuted && Volume <= 0)
            {
                Volume = (previousVolume > 0) ? previousVolume : defaultVolume;
            }
        }


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
            AudioSource audioSource = GetAudioSourceFromPool();            

            // Create new if no matching source was found in Pool
            if (audioSource == null)
            {
                sources.Add(audioSourceFactory.Create(Volume));
                audioSource = sources.Last();                
            }

            return audioSource;
        }


        // Gets the next free audio source from the pool
        // Returns first audiosource if set to single source mode
        private AudioSource GetAudioSourceFromPool()
        {            
            if (useSingleSource)
            {
                return sources.FirstOrDefault();
            }

            return sources.FirstOrDefault(e => !e.isPlaying);
        }

        #endregion
    }
}
