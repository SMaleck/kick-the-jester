using Assets.Source.App.Persistence;
using UniRx;
using UnityEngine;

namespace Assets.Source.App.Audio
{  
    public class AudioService
    {
        private readonly UserSettingsContext userSettings;
        private AudioSourceFactory audioSourceFactory;

        public AudioChannel BGMChannel { get; private set; }
        private const float DEFAULT_BGM_VOLUME = 1.0f;

        public AudioChannel SFXChannel { get; private set; }
        private const float DEFAULT_SFX_VOLUME = 0.8f;


        public AudioService(UserSettingsContext userSettings)
        {
            this.userSettings = userSettings;
            audioSourceFactory = new AudioSourceFactory();            

            float BGMVolume = userSettings.MuteBGM ? 0 : DEFAULT_BGM_VOLUME;
            BGMChannel = new AudioChannel(audioSourceFactory, DEFAULT_BGM_VOLUME, BGMVolume, true);

            float SFXVolume = userSettings.MuteSFX ? 0 : DEFAULT_SFX_VOLUME;
            SFXChannel = new AudioChannel(audioSourceFactory, DEFAULT_SFX_VOLUME, SFXVolume, false);

            // Listeners for Player Settings
            userSettings.MuteBGMProperty
                        .Subscribe((bool value) => { BGMChannel.IsMuted = value; });

            userSettings.MuteSFXProperty
                        .Subscribe((bool value) => { SFXChannel.IsMuted = value; });
        }


        /* -------------------------------------- BGM CHANNEL -------------------------------------- */
        public void PlayBGM(AudioClip clip)
        {
            BGMChannel.PlayClip(clip);
        }

        public void PlayLoopingBGM(AudioClip clip)
        {
            BGMChannel.PlayClip(clip, true, false);
        }


        /* -------------------------------------- SFX CHANNEL -------------------------------------- */
        public void PlaySFX(AudioClip clip)
        {
            SFXChannel.PlayClip(clip);
        }

        public void PlayRandomizedSFX(AudioClip clip)
        {
            SFXChannel.PlayClip(clip, false, true);
        }
    }
}
