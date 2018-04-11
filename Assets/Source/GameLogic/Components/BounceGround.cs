using System;
using UnityEngine;

namespace Assets.Source.GameLogic.Components
{
    public class BounceGround : BaseGameLogicComponent
    {
        private Transform OwnerLocation;

        // Exposed in Editor
        public GameObject Target;

        // Set in Startup
        private Rigidbody TargetBody;
        private Transform TargetLocation;


        // Set up the body which we will bounce later
        void Start()
        {
            // Get owner's transform
            OwnerLocation = gameObject.transform;

            if (Target == null)
            {
                throw new ArgumentNullException("Target cannot is NULL, cannot finish self-setup!");
            }

            // Get Target's Body        
            TargetBody = Target.GetComponent<Rigidbody>();

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
                OwnerLocation.position = new Vector3(TargetLocation.position.x, OwnerLocation.position.y, TargetLocation.position.z);
            }
        }
    }
}