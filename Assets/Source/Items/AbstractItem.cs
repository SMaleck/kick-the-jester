using Assets.Source.Behaviours.Jester;
using UnityEngine;

namespace Assets.Source.Items
{
    public abstract class AbstractItem : MonoBehaviour
    {
        [SerializeField]
        private AudioClip soundEffect;
        private AudioSource audioSource;


        protected abstract void Execute(Jester jester);


        // SETUP
        protected virtual void Awake()
        {
            // Create the Audio Source if an aeffect was set
            if(soundEffect != null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = soundEffect;
            }            
        }

       
        // Self-Destruct if we moved out of the camera view
        public virtual void LateUpdate()
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
            Jester jester = collision.gameObject.GetComponent<Jester>();

            if (jester != null)
            {
                TryPlaySound();
                Execute(jester);

                // Disable this trigger
                gameObject.GetComponent<Collider2D>().enabled = false;
            }            
        }


        // Checks whether this gameobject is still visible by the camera
        protected bool IsOutOfCameraBounds()
        {
            return gameObject.transform.position.x <= Camera.main.transform.position.x - (App.Cache.cameraWidth / 2f);
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
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
