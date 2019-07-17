using Assets.Source.App;
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

    [CreateAssetMenu(fileName = "JesterSpriteEffectsConfig", menuName = Constants.PROJECT_MENU_ROOT + "/JesterSpriteEffectsConfig")]
    public class JesterSpriteEffectsConfig : ScriptableObject
    {
        public float MinRotationSpeed = 1f;
        public float MaxRotationSpeed = 100f;

        [SerializeField] private List<JesterSpriteConfig> _jesterSpriteConfigs;

        public JesterSpriteConfig GetJesterSpriteConfigForLevel(int level)
        {
            return _jesterSpriteConfigs.First(e => e.JesterLevel == level);
        }
    }
}
