using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Config
{
    [Serializable]
    public class JesterSpriteConfig
    {
        public int JesterLevel;
        public Sprite[] ImpactSpritePool;
        public Sprite LaunchSprite;
        public Sprite LandingSprite;
    }

    [CreateAssetMenu(fileName = "JesterSpriteEffectsConfig", menuName = "KTJ/Config/JesterSpriteEffectsConfig")]
    public class JesterSpriteEffectsConfig : ScriptableObject
    {        
        public float MinRotationSpeed = 1f;
        public float MaxRotationSpeed = 100f;

        public GameObject PfxImpact;
        public GameObject PfxKick;

        [SerializeField] private List<JesterSpriteConfig> _jesterSpriteConfigs;

        public JesterSpriteConfig GetJesterSpriteConfigForLevel(int level)
        {
            return _jesterSpriteConfigs.First(e => e.JesterLevel == level);
        }
    }
}
