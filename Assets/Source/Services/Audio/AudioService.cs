using System.Linq;
using Assets.Source.Util.Poolable;
using UniRx;
using UnityEngine;


namespace Assets.Source.Services.Audio
{    
    // ToDo AudioService react to Mute/Unmute
    public class AudioService
    {
        private readonly SettingsService _settingsService;        

        private readonly ResourcePool<PoolableAudioSource> _musicChannel;
        private readonly ResourcePool<PoolableAudioSource> _effectChannel;

        // ToDo [CONFIG] Move to config SO
        private const float MIN_PITCH = 0.65f;
        private const float MAX_PITCH = 1.5f;


        public AudioService(SettingsService settingsService)
        {
            _settingsService = settingsService;            

            _musicChannel = new ResourcePool<PoolableAudioSource>(new AudioResourceFactory(), 1);
            _effectChannel = new ResourcePool<PoolableAudioSource>(new AudioResourceFactory());

            _settingsService.MusicVolume.Subscribe(volume => UpdateVolume(_musicChannel, volume));
            _settingsService.EffectsVolume.Subscribe(volume => UpdateVolume(_effectChannel, volume));
        }


        private void UpdateVolume(ResourcePool<PoolableAudioSource> channel, float volume)
        {
            channel.ForEach(e => e.Volume = volume);
        }


        private IStoppable PlayOn(ResourcePool<PoolableAudioSource> channel, AudioClip clip, bool loop, bool randomizePitch)
        {
            var slot = channel.GetFreeSlot();

            if (randomizePitch)
            {
                float pitch = UnityEngine.Random.Range(MIN_PITCH, MAX_PITCH);
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

        public IStoppable PlayMusic(AudioClip clip, bool loop = true)
        {
            return PlayOn(_musicChannel, clip, loop, false);
        }


        public IStoppable PlayEffect(AudioClip clip, bool loop = false)
        {
            return PlayOn(_effectChannel, clip, loop, false);
        }


        public IStoppable PlayEffectRandomized(AudioClip clip, bool loop = false)
        {
            return PlayOn(_effectChannel, clip, loop, true);
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
    }
}
