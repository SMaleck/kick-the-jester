using Assets.Source.Util;
using Assets.Source.Util.MonoObjectPooling;
using UnityEngine;
using Zenject;

namespace Assets.Source.Services.Particles
{
    public class ParticlePoolItem : AbstractMonoBehaviour, IPoolItem
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, ParticlePoolItem> { }

        [SerializeField] private ParticleSystem _particleSystem;

        [SerializeField] private ParticleEffectType _particleEffectType;
        public ParticleEffectType ParticleEffectType => _particleEffectType;

        public bool IsPaused { get; private set; }
        public bool IsFree => !_particleSystem.isPlaying;

        public void Play()
        {
            IsPaused = false;
            _particleSystem.Play();
        }

        public void Stop()
        {
            IsPaused = false;
            _particleSystem.Stop();
        }

        public void Pause()
        {
            if (IsPaused || !_particleSystem.isPlaying) { return; }

            IsPaused = true;
            _particleSystem.Pause(true);
        }

        public void Resume()
        {
            if (!IsPaused) { return; }

            IsPaused = false;
            _particleSystem.Play();
        }
    }
}
