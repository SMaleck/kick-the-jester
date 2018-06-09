using Assets.Source.App.ParticleEffects;
using Assets.Source.Config;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class SpriteEffect : AbstractComponent<JesterContainer>
    {
        private enum AnimState { None, Idle };
        private AnimationComponent<AbstractBehaviour> animationComponent;        

        private readonly PfxService pfxService;
        private readonly JesterSpriteEffectsConfig config;

        // Game Object for the Jester's sprite
        private readonly GameObject goJesterSprite;                
        private SpriteRenderer jesterSprite;

        private bool listenForImpacts = false;
        private bool isRotating = false;

        private float currentRotationSpeed = 0;
        private Vector3 rotationDirection = new Vector3(0, 0, -1);


        // GameObject for the Effects Sprite
        private readonly GameObject goEffectSprite;
        private Animator effectAnimator;        
        


        public SpriteEffect(JesterContainer owner, Animator animator, GameObject goJesterSprite, GameObject goEffectSprite, JesterSpriteEffectsConfig config, PfxService pfxService)
            : base(owner, true)
        {            
            this.pfxService = pfxService;
            this.config = config;

            animationComponent = new AnimationComponent<AbstractBehaviour>(owner, animator);

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
            owner.Collisions.OnGround(OnGroundHit);
            owner.Collisions.OnBoost(ModulateMainSprite);
        }


        protected override void Update()
        {
            if (isRotating)
            {
                goJesterSprite.transform.Rotate(rotationDirection * currentRotationSpeed * Time.deltaTime);
            }
        }


        private void OnKickHit()
        {
            // Stop Idle Animation
            animationComponent.Play(AnimState.None.ToString());

            jesterSprite.sprite = config.LaunchSprite;

            // Play Particle Effect
            pfxService.PlayAt(config.PfxKick, owner.Slot_KickTouch.position);
        }


        private void OnGroundHit()
        {
            ModulateMainSprite();

            // Play Particle Effect
            pfxService.PlayAt(config.PfxImpact, owner.Slot_GroundTouch.position);
        }


        private void OnShotHit()
        {            
            effectAnimator.Play("Anim_Projectile_Shoot");
            ModulateMainSprite();
        }


        private void OnLanded()
        {
            // Stop rotating and rest
            listenForImpacts = isRotating = false;
            goJesterSprite.transform.rotation = new Quaternion(0, 0, 0, 0);

            // Switch Sprite            
            jesterSprite.sprite = config.LandingSprite;

            // Start Idle Animation
            animationComponent.Play(AnimState.Idle.ToString());
        }



        // Sets a new roattion and switches the sprite to another random one
        private void ModulateMainSprite()
        {
            if (!listenForImpacts) { return; }            

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
}
