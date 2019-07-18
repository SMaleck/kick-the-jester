using Assets.Source.Util.MonoObjectPooling;

namespace Assets.Source.Services.Audio
{
    public class AudioService
    {
        private readonly AudioConfig _audioConfig;

        private readonly MonoObjectPool<AudioPoolItem> _musicChannel;
        private readonly MonoObjectPool<AudioPoolItem> _effectChannel;
        private readonly MonoObjectPool<AudioPoolItem> _uiChannel;

        public AudioService(
            AudioConfig config,
            AudioPoolItem.Factory audioPoolItemFactory)
        {
            _audioConfig = config;

            _musicChannel = new MonoObjectPool<AudioPoolItem>(audioPoolItemFactory, 1);
            _effectChannel = new MonoObjectPool<AudioPoolItem>(audioPoolItemFactory);
            _uiChannel = new MonoObjectPool<AudioPoolItem>(audioPoolItemFactory);
        }


        private void PlayOn(MonoObjectPool<AudioPoolItem> channel, AudioClipType audioClipType, float volume, bool loop, bool randomizePitch)
        {
            if (audioClipType.IsNone())
            {
                return;
            }

            var audioClip = _audioConfig.GetAudioClip(audioClipType);
            var audioPoolItem = channel.GetItem(_audioConfig.AudioSourcePrefab);

            if (randomizePitch)
            {
                float pitch = UnityEngine.Random.Range(_audioConfig.MinPitch, _audioConfig.MaxPitch);
                audioPoolItem.Play(audioClip, loop, pitch);
            }
            else
            {
                audioPoolItem.Play(audioClip, loop);
            }

            audioPoolItem.Volume = volume;
        }

        public void ResetPausedSlots()
        {
            ResetPausedSlots(_musicChannel);
            ResetPausedSlots(_effectChannel);
            ResetPausedSlots(_uiChannel);
        }

        private void ResetPausedSlots(MonoObjectPool<AudioPoolItem> pool)
        {
            pool.ForEach(item =>
            {
                if (item.IsPaused)
                {
                    item.Stop();
                }
            });
        }


        #region PLAY INTERFACE

        public void PlayMusic(AudioClipType audioClipType, bool loop = true)
        {
            PlayOn(_musicChannel, audioClipType, _musicVolume, loop, false);
        }

        public void PlayEffect(AudioClipType audioClipType, bool loop = false)
        {
            PlayOn(_effectChannel, audioClipType, _effectsVolume, loop, false);
        }

        public void PlayEffectRandomized(AudioClipType audioClipType, bool loop = false)
        {
            PlayOn(_effectChannel, audioClipType, _effectsVolume, loop, true);
        }

        public void PlayUiEffect(AudioClipType audioClipType, bool loop = false)
        {
            PlayOn(_uiChannel, audioClipType, _effectsVolume, loop, false);
        }

        #endregion


        #region PAUSE INTERFACE

        public void PauseEffects(bool isPaused)
        {
            if (isPaused)
            {
                Pause(_effectChannel);
            }
            else
            {
                Resume(_effectChannel);
            }
        }


        private void Pause(MonoObjectPool<AudioPoolItem> pool)
        {
            pool.ForEach(item => { item.Pause(); });
        }

        private void Resume(MonoObjectPool<AudioPoolItem> pool)
        {
            pool.ForEach(item => { item.Resume(); });
        }

        #endregion


        #region VOLUME INTERFACE

        private float _musicVolume = 1;
        public void SetMusicVolume(float volume)
        {
            _musicVolume = volume;
            _musicChannel.ForEach(e => e.Volume = volume);
        }

        private float _effectsVolume = 1;
        public void SetEffectsVolume(float volume)
        {
            _effectsVolume = volume;
            _effectChannel.ForEach(e => e.Volume = volume);
            _uiChannel.ForEach(e => e.Volume = volume);
        }

        #endregion
    }
}
