using Assets.Source.App;
using Assets.Source.Structs;
using UnityEngine;

namespace Assets.Source.Entities.Components
{
    public class FlightRecorder : BaseComponent
    {
        private Jester jester; 

        private Transform targetTransform;
        private Rigidbody2D targetBody;
        private Vector3 Origin;        
        
        public int HeightMeters { get; private set; }
        public int DistanceMeters { get; private set; }
        public float Velocity { get; private set; }

        public bool HasMoved { get; private set; }

        public bool IsMoving
        {
            get
            {
                return !MathUtil.NearlyEqual(Velocity, 0);
            }
        }

        public bool IsLanded
        {
            get
            {
                return HasMoved && !IsMoving;
            }
        }

        public void Start()
        {
            Jester jester = Entity as Jester;

            targetTransform = jester.goTransform;
            targetBody = jester.Body;
            Origin = targetTransform.position;            
        }

        public void LateUpdate()
        {
            float distanceUnits = MathUtil.Difference(Origin.x, targetTransform.position.x);
            DistanceMeters = (int)(distanceUnits * Constants.UNIT_TO_METERS_FACTOR);

            float heightUnits = MathUtil.Difference(Origin.y, targetTransform.position.y);
            HeightMeters = (int)(heightUnits * Constants.UNIT_TO_METERS_FACTOR);

            Velocity = MathUtil.CappedFloat(targetBody.velocity.magnitude);

            HasMoved = !MathUtil.NearlyEqual(distanceUnits, 0);
        }


        // Gets the current flight stats
        public FlightStats GetFlightStats()
        {
            return new FlightStats
            {
                Distance = DistanceMeters,
                Height = HeightMeters,
                Velocity = Velocity,                
                IsLanded = IsLanded,
            };
        }
    }
}
