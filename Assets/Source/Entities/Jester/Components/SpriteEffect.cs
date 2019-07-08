using Assets.Source.Entities.GenericComponents;
using Assets.Source.Entities.Jester.Config;
using Assets.Source.Services.Particles;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Components
{
    public class SpriteEffect : AbstractPausableComponent<JesterEntity>
    {
        private enum AnimState { None, Idle };

        private readonly JesterSpriteEffectsConfig _config;
        private readonly JesterSpriteConfig _spriteConfig;
        private readonly ParticleService _particleService;        

        private bool _listenForImpacts;
        private bool _isRotating;

        private float _currentRotationSpeed = 0;
        private readonly Vector3 _rotationDirection = new Vector3(0, 0, -1);

        public SpriteEffect(JesterEntity owner, JesterSpriteEffectsConfig config, ParticleService particleService) 
            : base(owner)
        {
            _config = config;
            _spriteConfig = config.GetJesterSpriteConfigForLevel(0);
            _particleService = particleService;

            Observable.EveryFixedUpdate()
                .Where(_ => !IsPaused.Value)
                .Subscribe(_ => OnUpdate())
                .AddTo(owner);

            owner.OnKicked
                .Subscribe(_ => OnKicked())
                .AddTo(owner);

            Owner.OnShot
                .Subscribe(_ => OnShot())
                .AddTo(Owner);

            Owner.Collisions.OnGround
                .Where(_ => _listenForImpacts)
                .Subscribe(_ => OnGround())
                .AddTo(Owner);

            Owner.Collisions.OnBoost
                .Subscribe(_ => OnBoost())
                .AddTo(Owner);

            IsPaused
                .Subscribe(OnPause)
                .AddTo(owner);
            
            owner.OnLanded
                .Subscribe(_ => OnLanded())
                .AddTo(owner);
        }


        private void OnPause(bool isPaused)
        {
            Owner.BodyAnimator.enabled = !isPaused;
            Owner.ProjectileAnimator.enabled = !isPaused;
        }


        private void OnUpdate()
        {
            if (!IsPaused.Value && _isRotating)
            {
                Owner.GoBodySprite.transform.Rotate(_rotationDirection * _currentRotationSpeed * Time.deltaTime);
            }
        }


        private void OnKicked()
        {
            // Stop Idle Animation
            Owner.BodyAnimator.Play(AnimState.None.ToString());
            Owner.BodySprite.sprite = _spriteConfig.LaunchSprite;

            // Play Particle Effect
            _particleService.PlayAt(_config.PfxKick, Owner.EffectSlotKick.position);

            _listenForImpacts = true;
        }

        private void OnGround()
        {
            ModulateMainSprite();

            // Play Particle Effect
            _particleService.PlayAt(_config.PfxImpact, Owner.EffectSlotGround.position);
        }

        private void OnBoost()
        {
            ModulateMainSprite();
        }

        private void OnShot()
        {
            Owner.ProjectileAnimator.Play("Anim_Projectile_Shoot");
            ModulateMainSprite();
        }

        private void OnLanded()
        {
            // Stop rotating and rest
            _listenForImpacts = _isRotating = false;
            Owner.BodySprite.transform.rotation = new Quaternion(0, 0, 0, 0);

            // Switch Sprite            
            Owner.BodySprite.sprite = _spriteConfig.LandingSprite;

            // Start Idle Animation
            Owner.BodyAnimator.Play(AnimState.Idle.ToString());
        }
        
        private void ModulateMainSprite()
        {
            if (!_listenForImpacts) { return; }

            // Set rotation
            _isRotating = true;
            _currentRotationSpeed = UnityEngine.Random.Range(_config.MinRotationSpeed, _config.MaxRotationSpeed);

            // Switch Sprite
            // Get all sprites that are not the one currently used, and get a random index from that
            var currentPool = _spriteConfig.ImpactSpritePool.Where(e => !e.Equals(Owner.BodySprite.sprite)).ToArray();   
            int index = Random.Range(0, currentPool.Length);

            Owner.BodySprite.sprite = currentPool[index];
        }
    }
}
