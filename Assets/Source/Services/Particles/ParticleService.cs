using Assets.Source.Util.Poolable;
using UnityEngine;

namespace Assets.Source.Services.Particles
{
    // ToDo Pause all particles on pause
    public class ParticleService
    {
        private readonly PrefabResourcePool<PoolableParticleSystem> _particleSystems;

        public ParticleService()
        {
            _particleSystems = new PrefabResourcePool<PoolableParticleSystem>(new ParticleFactory());
        }


        public void PlayAt(GameObject pfx, Vector3 position)
        {
            PoolableParticleSystem slot = _particleSystems.GetFreeSlotFor(pfx);

            slot.Position = position;
            slot.Play();
        }
    }
}
