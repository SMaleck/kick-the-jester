using Assets.Source.App.Configuration;
using Assets.Source.Util.Poolable;
using UnityEngine;


namespace Assets.Source.Services.Audio
{
    public class AudioService
    {
        private readonly AudioConfig _config;
        private readonly ResourcePool<PoolableAudioSource> _musicChannel;
        private readonly ResourcePool<PoolableAudioSource> _effectChannel;
        private readonly ResourcePool<PoolableAudioSource> _uiChannel;


        public AudioService(AudioConfig config)
        {
            _config = config;

            _musicChannel = new ResourcePool<PoolableAudioSource>(new AudioResourceFactory(), 1);
            _effectChannel = new ResourcePool<PoolableAudioSource>(new AudioResourceFactory());
            _uiChannel = new ResourcePool<PoolableAudioSource>(new AudioResourceFactory());
        }


        private PoolableAudioSource PlayOn(ResourcePool<PoolableAudioSource> channel, AudioClip clip, bool loop, bool randomizePitch)
        {
            var slot = channel.GetFreeSlot();

            if (randomizePitch)
            {
                float pitch = UnityEngine.Random.Range(_config.MinPitch, _config.MaxPitch);
                slot.Play(clip, loop, pitch);
            }
            else
            {
                slot.Play(clip, loop);
            }

            return slot;
        }

        public void ResetPausedSlots()
        {
            ResetPausedSlots(_musicChannel);
            ResetPausedSlots(_effectChannel);
        }

        private void ResetPausedSlots(ResourcePool<PoolableAudioSource> pool)
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

        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            var audioSource = PlayOn(_musicChannel, clip, loop, false);
            audioSource.Volume = _musicVolume;
        }

        public void PlayEffect(AudioClip clip, bool loop = false)
        {
            var audioSource = PlayOn(_effectChannel, clip, loop, false);
            audioSource.Volume = _effectsVolume;
        }

        public void PlayEffectRandomized(AudioClip clip, bool loop = false)
        {
            var audioSource = PlayOn(_effectChannel, clip, loop, true);
            audioSource.Volume = _effectsVolume;
        }

        public void PlayUiEffect(AudioClip clip, bool loop = false)
        {
            var audioSource = PlayOn(_uiChannel, clip, loop, false);
            audioSource.Volume = _effectsVolume;
        }

        #endregion


        #region PAUSE INTERFACE

        public void PauseMusic(bool isPaused)
        {
            if (isPaused)
            {
                Pause(_musicChannel);
            }
            else
            {
                Resume(_musicChannel);
            }
        }

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

        public void PauseAll(bool isPaused)
        {
            if (isPaused)
            {
                Pause(_effectChannel);
                Pause(_musicChannel);
            }
            else
            {
                Resume(_effectChannel);
                Resume(_musicChannel);
            }
        }


        private void Pause(ResourcePool<PoolableAudioSource> pool)
        {
            pool.ForEach(item => { item.Pause(); });
        }

        private void Resume(ResourcePool<PoolableAudioSource> pool)
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
