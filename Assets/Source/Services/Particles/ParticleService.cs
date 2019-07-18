using Assets.Source.Util.MonoObjectPooling;
using UniRx;
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
                pfxPrefab,
                item => item.ParticleEffectType == particleEffectType);

            particlePoolItem.Position = position;

            // ToDo Call play only on next frame
            particlePoolItem.Play();
        }

        public void ResetAll()
        {
            _particlePool.ForEach(item =>
            {
                item.Stop();
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
