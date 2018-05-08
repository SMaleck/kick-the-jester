using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Source.GameLogic.Audio
{
    public class AudioChannel : MonoBehaviour
    {
        private List<AudioSource> sources = new List<AudioSource>();

        private readonly float minPitch = 0.65f;
        private readonly float maxPitch = 1.5f;

        private const float DEFAULT_VOLUME = 0.8f;
        private FloatReactiveProperty volumeProperty = new FloatReactiveProperty(DEFAULT_VOLUME);

        public float Volume
        {
            get { return volumeProperty.Value; }
            set { volumeProperty.Value = Mathf.Clamp01(value); }
        }
        

        private void Awake()
        {
            volumeProperty.TakeUntilDestroy(this).Subscribe(_ => UpdateAudioSourceVolumes());
        }


        /* --------------------------------------------------------------------------------------- */
        #region VOLUME MANAGEMENT

        public void ToggleMuted()
        {
            Volume = (Volume <= 0) ? DEFAULT_VOLUME : 0;
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
            audioSource.loop = true;

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
                audioSource = AddNewAudioSource();
            }

            return audioSource;
        }


        private AudioSource AddNewAudioSource()
        {
            sources.Add(gameObject.AddComponent<AudioSource>());

            AudioSource audioSource = sources.Last();
            audioSource.volume = Volume;

            return audioSource;
        }


        #endregion
    }
}
