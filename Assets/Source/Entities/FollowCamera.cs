using UnityEngine;

namespace Assets.Source.Entities
{
    public class FollowCamera : BaseEntity
    {
        // Exposed in Editor
        public GameObject Target;

        private Transform targetLocation;

        // Use this for initialization
        void Start()
        {            
            targetLocation = Target.GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            FollowTarget();
        }


        private void FollowTarget()
        {
            if (Target != null)
            {
                Vector3 TPos = targetLocation.position;

                goTransform.position = new Vector3(TPos.x, TPos.y, goTransform.position.z);
            }
        }
    }
}