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
        private readonly ParticleService _particleService;        

        private bool _listenForImpacts;
        private bool _isRotating;

        private float _currentRotationSpeed = 0;
        private readonly Vector3 _rotationDirection = new Vector3(0, 0, -1);
                

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

            owner.Collisions.OnBoost
                .Subscribe(_ => OnBoost())
                .AddTo(owner);

            IsPaused
                .Subscribe(OnPause)
                .AddTo(owner);
            
            owner.OnLanded
                .Subscribe(_ => OnLanded())
                .AddTo(owner);
        }


        private void OnPause(bool isPaused)
        {
            owner.BodyAnimator.enabled = !isPaused;
            owner.ProjectileAnimator.enabled = !isPaused;
        }


        private void OnUpdate()
        {
            if (!IsPaused.Value && _isRotating)
            {
                owner.GoBodySprite.transform.Rotate(_rotationDirection * _currentRotationSpeed * Time.deltaTime);
            }
        }


        private void OnKicked()
        {
            // Stop Idle Animation
            owner.BodyAnimator.Play(AnimState.None.ToString());
            owner.BodySprite.sprite = _config.LaunchSprite;

            // Play Particle Effect
            _particleService.PlayAt(_config.PfxKick, owner.EffectSlotKick.position);

            _listenForImpacts = true;
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
            owner.ProjectileAnimator.Play("Anim_Projectile_Shoot");
            ModulateMainSprite();
        }

        private void OnLanded()
        {
            // Stop rotating and rest
            _listenForImpacts = _isRotating = false;
            owner.BodySprite.transform.rotation = new Quaternion(0, 0, 0, 0);

            // Switch Sprite            
            owner.BodySprite.sprite = _config.LandingSprite;

            // Start Idle Animation
            owner.BodyAnimator.Play(AnimState.Idle.ToString());
        }
        
        private void ModulateMainSprite()
        {
            if (!_listenForImpacts) { return; }

            // Set rotation
            _isRotating = true;
            _currentRotationSpeed = UnityEngine.Random.Range(_config.MinRotationSpeed, _config.MaxRotationSpeed);

            // Switch Sprite
            // Get all sprites that are not the one currently used, and get a random index from that
            var currentPool = _config.ImpactSpritePool.Where(e => !e.Equals(owner.BodySprite.sprite)).ToArray();   
            int index = Random.Range(0, currentPool.Length);

            owner.BodySprite.sprite = currentPool[index];
        }
    }
}
