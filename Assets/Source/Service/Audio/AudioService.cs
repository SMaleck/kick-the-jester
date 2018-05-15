using UniRx;
using UnityEngine;

namespace Assets.Source.Service.Audio
{
    public enum AudioChannels { BGM, SFX };

    public class AudioService
    {
        private AudioSourceFactory audioSourceFactory;

        public AudioChannel BGMChannel { get; private set; }
        private const float DEFAULT_BGM_VOLUME = 1.0f;

        public AudioChannel SFXChannel { get; private set; }
        private const float DEFAULT_SFX_VOLUME = 0.8f;


        public AudioService()
        {
            audioSourceFactory = new AudioSourceFactory();

            float BGMVolume = App.Cache.RepoRx.UserSettingsRepository.MuteBGM ? 0 : DEFAULT_BGM_VOLUME;
            BGMChannel = new AudioChannel(audioSourceFactory, DEFAULT_BGM_VOLUME, BGMVolume, true);

            float SFXVolume = App.Cache.RepoRx.UserSettingsRepository.MuteSFX ? 0 : DEFAULT_SFX_VOLUME;
            SFXChannel = new AudioChannel(audioSourceFactory, DEFAULT_SFX_VOLUME, SFXVolume, false);

            // Listeners for Player Settings
            App.Cache.RepoRx.UserSettingsRepository.MuteBGMProperty
                                                   .Subscribe((bool value) => { BGMChannel.IsMuted = value; });

            App.Cache.RepoRx.UserSettingsRepository.MuteSFXProperty
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
