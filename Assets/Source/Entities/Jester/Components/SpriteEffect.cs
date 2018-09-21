using Assets.Source.Entities.GenericComponents;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.App.ParticleEffects;
using Assets.Source.Behaviours;
using Assets.Source.Behaviours.Jester;
using Assets.Source.Entities.Jester.Config;
using Assets.Source.Services.Particles;
using UniRx;
using UnityEngine;
using Random = System.Random;

namespace Assets.Source.Entities.Jester.Components
{
    public class SpriteEffect : AbstractPausableComponent<JesterEntity>
    {
        private readonly JesterSpriteEffectsConfig _config;
        private readonly ParticleService _particleService;

        private bool listenForImpacts;
        private bool isRotating;

        private float currentRotationSpeed = 0;
        private Vector3 rotationDirection = new Vector3(0, 0, -1);


        public SpriteEffect(JesterEntity owner, JesterSpriteEffectsConfig config, ParticleService particleService) 
            : base(owner)
        {
            _config = config;
            _particleService = particleService;

            Observable.EveryFixedUpdate()
                .Where(_ => !IsPaused.Value)
                .Subscribe(_ => OnUpdate())
                .AddTo(owner);

            owner.OnKicked
                .Subscribe(_ => OnKicked())
                .AddTo(owner);

            owner.OnShot
                .Subscribe(_ => OnShot())
                .AddTo(owner);

            owner.Collisions.OnGround
                .Subscribe(_ => OnGround())
                .AddTo(owner);
        }


        private void OnUpdate()
        {
            if (isRotating)
            {
                owner.GoBodySprite.transform.Rotate(rotationDirection * currentRotationSpeed * Time.deltaTime);
            }
        }


        private void OnKicked()
        {
            // Stop Idle Animation
            animationComponent.Play(AnimState.None.ToString());

            jesterSprite.sprite = config.LaunchSprite;

            // Play Particle Effect
            pfxService.PlayAt(config.PfxKick, owner.Slot_KickTouch.position);
        }


        private void OnShot()
        {
            ModulateMainSprite();

            // Play Particle Effect
            pfxService.PlayAt(config.PfxImpact, owner.Slot_GroundTouch.position);
        }


        private void OnGround()
        {
            effectAnimator.Play("Anim_Projectile_Shoot");
            ModulateMainSprite();
        }

        // ---------------------------------------------------


        private enum AnimState { None, Idle };
        private AnimationComponent<AbstractBehaviour> animationComponent;


        // Game Object for the Jester's sprite
        private readonly GameObject goJesterSprite;
        private SpriteRenderer jesterSprite;

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
