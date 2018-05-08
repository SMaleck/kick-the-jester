using UniRx;
using UnityEngine;

namespace Assets.Source.GameLogic.Audio
{
    public enum AudioChannels { BGM, SFX };

    public class AudioService : MonoBehaviour
    {
        private AudioSourceFactory audioSourceFactory;

        private AudioChannel BGMChannel;
        private const float DEFAULT_BGM_VOLUME = 1.0f;

        private AudioChannel SFXChannel;
        private const float DEFAULT_SFX_VOLUME = 0.8f;


        private void Awake()
        {
            audioSourceFactory = gameObject.AddComponent<AudioSourceFactory>();
            BGMChannel = new AudioChannel(audioSourceFactory, DEFAULT_BGM_VOLUME, false);
            SFXChannel = new AudioChannel(audioSourceFactory, DEFAULT_SFX_VOLUME, false);

            // Listeners for Player Settings
            App.Cache.RepoRx.UserSettingsRepository.MuteBGMProperty.Subscribe((bool value) => { BGMChannel.IsMuted = value; });
            App.Cache.RepoRx.UserSettingsRepository.MuteSFXProperty.Subscribe((bool value) => { SFXChannel.IsMuted = value; });
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

        public void ToggleBGMMuted()
        {
            App.Cache.RepoRx.UserSettingsRepository.MuteBGM = !App.Cache.RepoRx.UserSettingsRepository.MuteBGM;
        }

        public bool IsBGMChannelMuted
        {
            get { return BGMChannel.IsMuted; }
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

        public void ToggleSFXMuted()
        {
            App.Cache.RepoRx.UserSettingsRepository.MuteSFX = !App.Cache.RepoRx.UserSettingsRepository.MuteSFX;
        }

        public bool IsSFXChannelMuted
        {
            get { return SFXChannel.IsMuted; }
        }
    }
}
