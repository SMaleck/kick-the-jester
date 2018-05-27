using Assets.Source.App;
using Assets.Source.Behaviours.Jester;
using UnityEngine;

namespace Assets.Source.Behaviours.Items
{
    public abstract class AbstractItem : MonoBehaviour
    {
        [SerializeField] protected bool isDestructible = true;
        [SerializeField] protected AudioClip soundEffect;        

        protected abstract void Execute(JesterContainer jester);

       
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
            JesterContainer jester = collision.gameObject.GetComponent<JesterContainer>();

            if (jester == null) { return; }

            TryPlaySound();
            Execute(jester);

            // Disable this trigger
            gameObject.GetComponent<Collider2D>().enabled = false;

            // Self Destroy if set
            if (isDestructible)
            {
                SelfDestruct();
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
