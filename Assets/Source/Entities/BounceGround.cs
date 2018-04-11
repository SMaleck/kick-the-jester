using System;
using UnityEngine;

namespace Assets.Source.Entities
{
    public class BounceGround : BaseEntity
    {
        // Exposed in Editor
        public GameObject Target;

        // Set in Startup        
        private Transform TargetLocation;


        // Set up the body which we will bounce later
        void Start()
        {
            if (Target == null)
            {
                throw new ArgumentNullException("Target cannot be NULL, cannot finish self-setup!");
            }

            // Get Target's transform
            TargetLocation = Target.GetComponent<Transform>();

            // Sync positions
            FollowTargetXAxis();
        }


        // Follow the target on the X-Axis
        void Update()
        {
            FollowTargetXAxis();
        }


        private void FollowTargetXAxis()
        {
            if (Target != null)
            {
                goTransform.position = new Vector3(TargetLocation.position.x, goTransform.position.y, TargetLocation.position.z);
            }
        }
    }
}