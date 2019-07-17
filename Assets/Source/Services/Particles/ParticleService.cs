using Assets.Source.Util.Poolable;
using UnityEngine;

namespace Assets.Source.Services.Particles
{
    // ToDo ResourcePools should be more generic, so PAUSE interface can be in base class
    public class ParticleService
    {
        private readonly ParticleEffectConfig _particleEffectConfig;
        private readonly PrefabResourcePool<PoolableParticleSystem> _particleSystems;

        public ParticleService(ParticleEffectConfig particleEffectConfig)
        {
            _particleEffectConfig = particleEffectConfig;
            _particleSystems = new PrefabResourcePool<PoolableParticleSystem>(new ParticleFactory());
        }

        public void PlayEffectAt(ParticleEffectType particleEffectType, Vector3 position)
        {
            if (particleEffectType == ParticleEffectType.None)
            {
                return;
            }

            var pfxPrefab = _particleEffectConfig.GetParticleEffectPrefab(particleEffectType);
            PoolableParticleSystem slot = _particleSystems.GetFreeSlotFor(pfxPrefab.gameObject);

            slot.Position = position;
            slot.Play();
        }


        public void PlayAt(GameObject pfx, Vector3 position)
        {
            var pfxX = _particleEffectConfig.GetParticleEffectPrefab(ParticleEffectType.BombExplosion).gameObject;
            PoolableParticleSystem slot = _particleSystems.GetFreeSlotFor(pfxX);            

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
