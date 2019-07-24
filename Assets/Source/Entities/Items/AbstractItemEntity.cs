using Assets.Source.Entities.Cameras;
using Assets.Source.Entities.Jester;
using Assets.Source.Services.Audio;
using Assets.Source.Services.Particles;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Assets.Source.Entities.Items
{
    // ToDo Move Item Values to config files
    public abstract class AbstractItemEntity : AbstractMonoEntity
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, AbstractItemEntity> { }

        [Header("Collisions")]
        [SerializeField] private bool isDestructible;
        [SerializeField] private Collider2D _collisionProbe;

        [Header("Effects")]
        [SerializeField] private AudioClipType _audioClipType;
        [SerializeField] private ParticleEffectType _particleEffectType;

        protected AudioService AudioService;
        protected ParticleService ParticleService;
        protected MainCamera Camera;

        protected abstract void Execute(JesterEntity jester);

        [Inject]
        private void Inject(
            AudioService audioService,
            ParticleService particleService,
            MainCamera mainCamera)
        {
            AudioService = audioService;
            ParticleService = particleService;
            Camera = mainCamera;
        }

        public override void Initialize()
        {
            Observable.EveryLateUpdate()
                .Subscribe(_ => OnLateUpdate())
                .AddTo(Disposer);

            _collisionProbe.OnTriggerEnter2DAsObservable()
                .Subscribe(OnCollisionProbeEntered)
                .AddTo(Disposer);

            Setup();
        }

        protected abstract void Setup();

        // Self-Destruct if we moved out of the camera view
        private void OnLateUpdate()
        {
            if (IsOutOfCameraBounds())
            {
                SelfDestruct();
            }
        }

        public void OnCollisionProbeEntered(Collider2D collision)
        {
            // Safety Check: Only execute if we collided with the Jester
            JesterEntity jester = collision.gameObject.GetComponent<JesterEntity>();

            if (jester == null) { return; }

            TryPlaySound();
            TryPlayParticleEffect();

            Execute(jester);

            // Disable this trigger
            _collisionProbe.enabled = false;

            // Self Destroy if set
            if (isDestructible)
            {
                SelfDestruct();
            }
        }

        protected bool IsOutOfCameraBounds()
        {
            return Position.x <= Camera.Position.x - (Camera.Width / 2f);
        }

        protected void SelfDestruct()
        {
            gameObject.SetActive(false);
            GameObject.Destroy(gameObject);
        }

        // Attempts to play the attached AudioSource
        protected void TryPlaySound()
        {
            AudioService.PlayEffect(_audioClipType);
        }

        // Attempts to play the attached Particle Effect
        protected void TryPlayParticleEffect()
        {
            ParticleService.PlayEffectAt(_particleEffectType, transform.position);
        }
    }
}
