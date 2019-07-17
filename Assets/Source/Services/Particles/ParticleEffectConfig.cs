using Assets.Source.App;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Services.Particles
{
    [CreateAssetMenu(fileName = "ParticleEffectConfig", menuName = Constants.PROJECT_MENU_ROOT + "/ParticleEffectConfig")]
    public class ParticleEffectConfig : ScriptableObject
    {
        [Serializable]
        private class ParticleEffectConfigItem
        {
            [SerializeField] private ParticleEffectType _particleEffectType;
            public ParticleEffectType ParticleEffectType => _particleEffectType;

            [SerializeField] private ParticleSystem _particleSystemPrefab;
            public ParticleSystem ParticleSystemPrefab => _particleSystemPrefab;
        }

        [SerializeField] private List<ParticleEffectConfigItem> _particleEffectConfigItems;

        public ParticleSystem GetParticleEffectPrefab(ParticleEffectType particleEffectType)
        {
            return _particleEffectConfigItems
                .First(item => item.ParticleEffectType == particleEffectType)
                .ParticleSystemPrefab;
        }
    }
}
