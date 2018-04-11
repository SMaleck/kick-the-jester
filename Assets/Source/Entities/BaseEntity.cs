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
            
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

