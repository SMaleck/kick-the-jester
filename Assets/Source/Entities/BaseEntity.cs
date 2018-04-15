using UnityEngine;

namespace Assets.Source.Entities
{
    public class BaseEntity : MonoBehaviour
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

