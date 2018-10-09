using Assets.Source.Util.Poolable;
using UnityEngine;

namespace Assets.Source.Services.Particles
{
    // ToDo ResourcePools should be more geenric, so PAUSE interface can be in base class
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

        public void ResetPausedSlots()
        {
            _particleSystems.ForEach(item =>
            {
                if (item.IsPaused)
                {
                    item.Stop();
                }
            });
        }


        #region PAUSE INTERFACE

        public void PauseAll(bool isPaused)
        {
            if (isPaused)
            {
                Pause(_particleSystems);                
            }
            else
            {
                Resume(_particleSystems);                
            }
        }

        private void Pause(PrefabResourcePool<PoolableParticleSystem> pool)
        {
            pool.ForEach(item => { item.Pause(); });
        }

        private void Resume(PrefabResourcePool<PoolableParticleSystem> pool)
        {
            pool.ForEach(item => { item.Resume(); });
        }

        #endregion
    }
}
