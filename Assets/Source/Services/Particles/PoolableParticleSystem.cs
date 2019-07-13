using Assets.Source.Util.Poolable;
using UnityEngine;

namespace Assets.Source.Services.Particles
{
    public class PoolableParticleSystem : INamedPoolableResource, IStoppable
    {
        private readonly ParticleSystem _particleSystem;
        public bool IsFree => !_particleSystem.isPlaying;
        public string Name => _particleSystem.gameObject.name;

        public bool IsPaused { get; private set; }

        public Vector3 Position
        {
            get { return _particleSystem.gameObject.transform.position; }
            set { _particleSystem.gameObject.transform.position = value; }
        }


        public PoolableParticleSystem(ParticleSystem particleSystem)
        {
            _particleSystem = particleSystem;
        }

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
