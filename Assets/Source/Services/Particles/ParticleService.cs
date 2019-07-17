using Assets.Source.Util.Poolable;
using UnityEngine;

namespace Assets.Source.Services.Particles
{    
    public class ParticleService
    {
        private readonly ParticleEffectConfig _particleEffectConfig;
        private readonly MonoObjectPool<ParticlePoolItem> _particlePool;

        public ParticleService(
            ParticleEffectConfig particleEffectConfig,
            ParticlePoolItem.Factory particlePoolItemFactory)
        {
            _particleEffectConfig = particleEffectConfig;
            _particlePool = new MonoObjectPool<ParticlePoolItem>(particlePoolItemFactory);
        }

        public void PlayEffectAt(ParticleEffectType particleEffectType, Vector3 position)
        {
            if (particleEffectType == ParticleEffectType.None)
            {
                return;
            }

            var pfxPrefab = _particleEffectConfig.GetParticleEffectPrefab(particleEffectType);
            var particlePoolItem = _particlePool.GetItem(
                item => item.ParticleEffectType == particleEffectType,
                pfxPrefab);

            particlePoolItem.Position = position;
            particlePoolItem.Play();
        }

        public void ResetPausedSlots()
        {
            _particlePool.ForEach(item =>
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
                _particlePool.ForEach(item => { item.Pause(); });
            }
            else
            {
                _particlePool.ForEach(item => { item.Resume(); });
            }
        }

        #endregion
    }
}
