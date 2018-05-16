using Assets.Source.AppKernel;
using Assets.Source.Behaviours.Jester;
using UnityEngine;

namespace Assets.Source.Items
{
    public abstract class AbstractItem : MonoBehaviour
    {
        [SerializeField] private AudioClip soundEffect;        

        protected abstract void Execute(Jester jester);

       
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
            return gameObject.transform.position.x <= App.Cache.MainCamera.UCamera.transform.position.x - (App.Cache.MainCamera.Width / 2f);
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
            if (soundEffect != null)
            {
                Kernel.AudioService.PlaySFX(soundEffect);
            }
        }
    }
}
