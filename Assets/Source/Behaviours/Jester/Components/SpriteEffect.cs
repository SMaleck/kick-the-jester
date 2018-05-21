using Assets.Source.Config;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class SpriteEffect : AbstractComponent<Jester>
    {
        private readonly JesterSpriteEffectsConfig config;

        private readonly GameObject goSprite;                
        private SpriteRenderer sprite;

        private bool listenForImpacts = false;
        private bool isRotating = false;

        private float currentRotationSpeed = 0;
        private Vector3 rotationDirection = new Vector3(0, 0, -1);


        public SpriteEffect(Jester owner, GameObject goSprite, JesterSpriteEffectsConfig config)
            : base(owner, true)
        {
            this.config = config;

            this.goSprite = goSprite;
            sprite = goSprite.GetComponent<SpriteRenderer>();            

            owner.IsStartedProperty.Where(e => e).Subscribe(_ => { listenForImpacts = true; }).AddTo(owner);
            owner.IsLandedProperty.Where(e => e).Subscribe(_ => OnLanded()).AddTo(owner);

            // Impact Listeners
            owner.Collisions.OnGround(OnImpact);
            owner.Collisions.OnBoost(OnImpact);
        }


        protected override void Update()
        {
            if (isRotating)
            {
                goSprite.transform.Rotate(rotationDirection * currentRotationSpeed * Time.deltaTime);
            }
        }


        private void OnImpact()
        {
            if (listenForImpacts)
            {
                // Set rotation
                isRotating = true;
                currentRotationSpeed = UnityEngine.Random.Range(config.MinRotationSpeed, config.MaxRotationSpeed);

                // Switch Sprite
                int index = Random.Range(0, config.ImpactSpritePool.Length);
                sprite.sprite = config.ImpactSpritePool[index];
            }
        }


        private void OnLanded()
        {
            // Stop rotating and rest
            listenForImpacts = isRotating = false;
            goSprite.transform.rotation = new Quaternion(0, 0, 0, 0);

            // Switch Sprite            
            sprite.sprite = config.LandingSprite;
        }
    }
}
