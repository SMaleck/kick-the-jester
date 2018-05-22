using UnityEngine;

namespace Assets.Source.Behaviours
{
    public abstract class AbstractBehaviour : MonoBehaviour
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
