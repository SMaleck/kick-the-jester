using Assets.Source.Config;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class SpriteEffect : AbstractComponent<JesterContainer>
    {
        private readonly JesterSpriteEffectsConfig config;

        // GAme Object for trhe Jester's sprite
        private readonly GameObject goJesterSprite;                
        private SpriteRenderer jesterSprite;

        private bool listenForImpacts = false;
        private bool isRotating = false;

        private float currentRotationSpeed = 0;
        private Vector3 rotationDirection = new Vector3(0, 0, -1);


        // GameObject for the Effects Sprite
        private readonly GameObject goEffectSprite;
        private Animator effectAnimator;        
        


        public SpriteEffect(JesterContainer owner, GameObject goJesterSprite, GameObject goEffectSprite, JesterSpriteEffectsConfig config)
            : base(owner, true)
        {
            this.config = config;

            // Setup Jester Sprite
            this.goJesterSprite = goJesterSprite;
            jesterSprite = this.goJesterSprite.GetComponent<SpriteRenderer>();

            // Setup Effects Sprite
            this.goEffectSprite = goEffectSprite;
            this.effectAnimator = this.goEffectSprite.GetComponent<Animator>();            

            // Event Listeners
            owner.IsStartedProperty.Where(e => e).Subscribe(_ => { listenForImpacts = true; }).AddTo(owner);
            owner.IsLandedProperty.Where(e => e).Subscribe(_ => OnLanded()).AddTo(owner);

            // Kick & Shot Listeners
            MessageBroker.Default.Receive<JesterEffects>()
                                 .Where(e => e.Equals(JesterEffects.Kick))
                                 .Subscribe(_ => OnKickHit())
                                 .AddTo(owner);

            MessageBroker.Default.Receive<JesterEffects>()
                                 .Where(e => e.Equals(JesterEffects.Shot))
                                 .Subscribe(_ => OnShotHit())
                                 .AddTo(owner);

            // Impact Listeners
            owner.Collisions.OnGround(OnImpact);
            owner.Collisions.OnBoost(OnImpact);            
        }


        protected override void Update()
        {
            if (isRotating)
            {
                goJesterSprite.transform.Rotate(rotationDirection * currentRotationSpeed * Time.deltaTime);
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

                // Get all sprites that are not the one currently used, and get a random index from that
                var currentPool = config.ImpactSpritePool.Where(e => !e.Equals(jesterSprite.sprite));
                int index = Random.Range(0, currentPool.Count());

                jesterSprite.sprite = currentPool.ElementAt(index);
            }
        }


        private void OnLanded()
        {
            // Stop rotating and rest
            listenForImpacts = isRotating = false;
            goJesterSprite.transform.rotation = new Quaternion(0, 0, 0, 0);

            // Switch Sprite            
            jesterSprite.sprite = config.LandingSprite;
        }


        private void OnKickHit()
        {
            jesterSprite.sprite = config.LaunchSprite;
        }


        private void OnShotHit()
        {            
            effectAnimator.Play("Anim_Projectile_Shoot");
            OnImpact();
        }
    }
}
