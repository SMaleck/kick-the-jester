using Assets.Source.Util.Poolable;
using UnityEngine;

namespace Assets.Source.Services.Particles
{
    public class PoolableParticleSystem : INamedPoolableResource, IStoppable
    {
        private readonly ParticleSystem _particleSystem;
        public bool IsFree => !_particleSystem.isPlaying;
        public string Name => _particleSystem.gameObject.name;

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
            _particleSystem.Play();
        }

        public void Stop()
        {
            _particleSystem.Stop();
        }

        public void Pause()
        {
            _particleSystem.Pause(true);
        }

        public void Resume()
        {
            _particleSystem.Play();
        }
    }
}
