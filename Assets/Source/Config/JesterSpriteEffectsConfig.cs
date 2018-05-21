using UnityEngine;

namespace Assets.Source.Config
{    
    public class JesterSpriteEffectsConfig : ScriptableObject
    {        
        public float MinRotationSpeed = 1f;
        public float MaxRotationSpeed = 100f;
        
        public Sprite[] ImpactSpritePool;
        public Sprite LandingSprite;
    }
}
