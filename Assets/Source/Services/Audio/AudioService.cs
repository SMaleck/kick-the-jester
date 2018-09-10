using Assets.Source.Util.Poolable;
using UniRx;
using UnityEngine;


namespace Assets.Source.Services.Audio
{
    public class AudioService
    {
        private readonly SettingsService _settingsService;        

        private readonly ResourcePool<PoolableAudioSource> _musicChannel;
        private readonly ResourcePool<PoolableAudioSource> _effectChannel;

        private const float MIN_PITCH = 0.65f;
        private const float MAX_PITCH = 1.5f;


        public AudioService(SettingsService settingsService)
        {
            _settingsService = settingsService;            

            _musicChannel = new ResourcePool<PoolableAudioSource>(new AudioResourceFactory());
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
    }
}
