using UnityEngine;

namespace Assets.Source.Entities.Jester.Config
{
    [CreateAssetMenu(fileName = "JesterSpriteEffectsConfig", menuName = "KTJ/Config/JesterSpriteEffectsConfig")]
    public class JesterSpriteEffectsConfig : ScriptableObject
    {        
        public float MinRotationSpeed = 1f;
        public float MaxRotationSpeed = 100f;
        
        public Sprite[] ImpactSpritePool;
        public Sprite LaunchSprite;
        public Sprite LandingSprite;

        public GameObject PfxImpact;
        public GameObject PfxKick;
    }
}
