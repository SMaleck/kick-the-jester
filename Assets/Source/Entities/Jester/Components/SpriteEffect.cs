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

namespace Assets.Source.Entities.Jester.Components
{
    public class SpriteEffect : AbstractPausableComponent<JesterEntity>
    {
        private enum AnimState { None, Idle };

        private readonly JesterSpriteEffectsConfig _config;
        private readonly ParticleService _particleService;

        private bool listenForImpacts;
        private bool isRotating;

        private float currentRotationSpeed = 0;
        private Vector3 rotationDirection = new Vector3(0, 0, -1);

        private Animator effectAnimator;
        private AnimationComponent<AbstractBehaviour> animationComponent;

        public SpriteEffect(JesterEntity owner, JesterSpriteEffectsConfig config, ParticleService particleService, Animator animator) 
            : base(owner)
        {
            _config = config;
            _particleService = particleService;

            // ToDo Relies on old component
            //animationComponent = new AnimationComponent<AbstractBehaviour>(owner, animator);

            // ToDo probably not the best solution
            effectAnimator = owner.GoEffectSprite.GetComponent<Animator>();

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

            owner.Collisions.OnBoost
                .Subscribe(_ => OnBoost())
                .AddTo(owner);

            // ToDo listen to started/landed            
            //owner.IsStartedProperty.Where(e => e).Subscribe(_ => { listenForImpacts = true; }).AddTo(owner);
            //owner.IsLandedProperty.Where(e => e).Subscribe(_ => OnLanded()).AddTo(owner);
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

            owner.BodySprite.sprite = _config.LaunchSprite;

            // Play Particle Effect
            _particleService.PlayAt(_config.PfxKick, owner.EffectSlotKick.position);
        }

        private void OnGround()
        {
            ModulateMainSprite();

            // Play Particle Effect
            _particleService.PlayAt(_config.PfxImpact, owner.EffectSlotGround.position);
        }

        private void OnBoost()
        {
            ModulateMainSprite();
        }

        private void OnShot()
        {
            effectAnimator.Play("Anim_Projectile_Shoot");
            ModulateMainSprite();
        }

        private void OnLanded()
        {
            // Stop rotating and rest
            listenForImpacts = isRotating = false;
            owner.BodySprite.transform.rotation = new Quaternion(0, 0, 0, 0);

            // Switch Sprite            
            owner.BodySprite.sprite = _config.LandingSprite;

            // Start Idle Animation
            animationComponent.Play(AnimState.Idle.ToString());
        }
        
        private void ModulateMainSprite()
        {
            if (!listenForImpacts) { return; }

            // Set rotation
            isRotating = true;
            currentRotationSpeed = UnityEngine.Random.Range(_config.MinRotationSpeed, _config.MaxRotationSpeed);

            // Switch Sprite
            // Get all sprites that are not the one currently used, and get a random index from that
            var currentPool = _config.ImpactSpritePool.Where(e => !e.Equals(owner.BodySprite.sprite));
            int index = Random.Range(0, currentPool.Count());

            owner.BodySprite.sprite = currentPool.ElementAt(index);
        }
    }
}
