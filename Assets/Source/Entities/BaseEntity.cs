using UnityEngine;

namespace Assets.Source.Entities
{
    public abstract class BaseEntity : MonoBehaviour
    {
        public Transform goTransform
        {
            get
            {
                return gameObject.transform;
            }
        }           
    }
}

