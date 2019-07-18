using Assets.Source.Entities.Cameras;
using Assets.Source.Entities.Jester;
using Assets.Source.Services.Audio;
using Assets.Source.Services.Particles;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Items
{
    public abstract class AbstractItemEntity : AbstractMonoEntity
    {
        [SerializeField] protected bool isDestructible = true;
        [SerializeField] protected AudioClipType _audioClipType;
        [SerializeField] protected ParticleEffectType _particleEffectType;

        protected AudioService AudioService;
        protected ParticleService ParticleService;
        protected MainCamera Camera;

        protected abstract void Execute(JesterEntity jester);

        // ToDo Instantiate so that injection can occur
        public virtual void Setup(AudioService audioService, ParticleService particleService, MainCamera mainCamera)
        {            
            AudioService = audioService;
            ParticleService = particleService;
            Camera = mainCamera;

            Observable.EveryLateUpdate()
                .Subscribe(_ => OnLateUpdate())
                .AddTo(this);
        }


        // Self-Destruct if we moved out of the camera view
        private void OnLateUpdate()
        {
            if (IsOutOfCameraBounds())
            {
                SelfDestruct();
            }
        }


        // COLLISION TRIGGER
        public void OnTriggerEnter2D(Collider2D collision)
        {
            // Safety Check: Only execute if we collided with the Jester
            JesterEntity jester = collision.gameObject.GetComponent<JesterEntity>();

            if (jester == null) { return; }

            TryPlaySound();
            TryPlayParticleEffect();
            Execute(jester);

            // Disable this trigger
            gameObject.GetComponent<Collider2D>().enabled = false;

            // Self Destroy if set
            if (isDestructible)
            {
                SelfDestruct();
            }
        }


        // ToDo Extract OutOfCameraBounds check into separate component
        // Checks whether this gameobject is still visible by the camera
        protected bool IsOutOfCameraBounds()
        {                        
            return Position.x <= Camera.Position.x - (Camera.Width / 2f);
        }


        // Self Destruct Mechanism
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
