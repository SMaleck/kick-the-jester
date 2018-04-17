using UnityEngine;

namespace Assets.Source.Entities.Items
{
    public abstract class AbstractItem : MonoBehaviour
    {
        // Self-Destruct if we moved out of the camer view
        public virtual void LateUpdate()
        {            
            if (gameObject.transform.position.x <= Camera.main.transform.position.x - (App.Cache.cameraWidth / 2f))
            {
                gameObject.SetActive(false);
                GameObject.Destroy(gameObject);
            }
        }        
    }
}
