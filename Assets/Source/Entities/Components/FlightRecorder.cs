using Assets.Source.App;
using UnityEngine;

namespace Assets.Source.Entities.Components
{
    class FlightRecorder : BaseComponent
    {
        private Jester jester; 

        private Transform targetTransform;
        private Rigidbody2D targetBody;
        private Vector3 Origin;        

        private float DistanceUnits;
        public int DistanceMeters { get; private set; }

        private float VelocityX;         

        public void Start()
        {
            Jester jester = Entity as Jester;

            targetTransform = jester.goTransform;
            targetBody = jester.Body;
            Origin = targetTransform.position;            
        }

        public void LateUpdate()
        {
            DistanceUnits = MathUtil.Difference(Origin.x, targetTransform.position.x);
            DistanceMeters = (int)(DistanceUnits * Constants.UNIT_TO_METERS_FACTOR);

            VelocityX = MathUtil.CappedFloat(targetBody.velocity.x);            
        }
    }
}
