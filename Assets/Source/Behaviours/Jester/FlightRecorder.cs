using Assets.Source.App;
using Assets.Source.Models;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester
{
    public class FlightRecorder : AbstractBodyBehaviour
    {
        #region RX-Ready Properties

        // IS LANDED
        private event NotifyEventHandler _OnLanded = delegate { };
        public void OnLanded(NotifyEventHandler handler)
        {
            _OnLanded += handler;            
        }

        private bool isLanded = false;
        public bool IsLanded
        {
            get { return isLanded; }
            private set
            {
                bool previous = isLanded;
                isLanded = value;
                
                // Notify if we landed and the value changed
                if (value && (isLanded != previous)) { _OnLanded(); }
            }
        }


        // STARTED FLIGHT
        private event NotifyEventHandler _OnStartedFlight = delegate { };
        public void OnStartedFlight(NotifyEventHandler handler)
        {
            _OnStartedFlight += handler;          
        }

        private bool isStared = false;
        public bool IsStared
        {
            get { return isStared; }
            private set
            {
                bool previous = isStared;
                isStared = value;

                // Notify if we landed and the value changed
                if (value && (isStared != previous)) { _OnStartedFlight(); }
            }
        }


        // DISTANCE
        private event IntValueEventHandler _OnDistanceChanged = delegate { };
        public void OnDistanceChanged(IntValueEventHandler handler)
        {
            _OnDistanceChanged += handler;
        }

        private int distanceMeters = 0;
        public int DistanceMeters
        {
            get { return distanceMeters; }
            private set
            {
                if (distanceMeters != value)
                {
                    distanceMeters = value;
                    _OnDistanceChanged(distanceMeters);
                }
            }
        }


        // HEIGHT
        private event IntValueEventHandler _OnHeightChanged = delegate { };
        public void OnHeightChanged(IntValueEventHandler handler)
        {
            _OnHeightChanged += handler;            
        }

        private int heightMeters = 0;
        public int HeightMeters
        {
            get { return heightMeters; }
            private set
            {
                if(heightMeters != value)
                {
                    heightMeters = value;
                    _OnHeightChanged(heightMeters);
                }
            }
        }


        // VELOCITY
        private event FloatValueEventHandler _OnVelocityChanged = delegate { };
        public void OnVelocityChanged(FloatValueEventHandler handler)
        {
            _OnVelocityChanged += handler;
        }

        private float velocityKmh = 0f;
        public float VelocityKmh
        {
            get { return velocityKmh; }
            private set
            {
                if (velocityKmh != value)
                {
                    velocityKmh = value;
                    _OnVelocityChanged(velocityKmh);
                }
            }
        }

        #endregion


        private Vector3 Origin;                        


        public void Start()
        {            
            Origin = goTransform.position;            
        }


        public void LateUpdate()
        {
            float distanceUnits = MathUtil.Difference(Origin.x, goTransform.position.x);
            DistanceMeters = (int)(distanceUnits * Constants.UNIT_TO_METERS_FACTOR);

            float heightUnits = MathUtil.Difference(Origin.y, goTransform.position.y);
            HeightMeters = (int)(heightUnits * Constants.UNIT_TO_METERS_FACTOR);

            VelocityKmh = MathUtil.CappedFloat(goBody.velocity.magnitude);            

            // Check if Flight has Started
            IsStared = !MathUtil.NearlyEqual(distanceUnits, 0);

            // Check if Jester has landed
            IsLanded = IsStared && MathUtil.NearlyEqual(VelocityKmh, 0);
        }


        // Gets the current flight stats
        public FlightStats GetFlightStats()
        {
            return new FlightStats
            {
                Distance = DistanceMeters,
                Height = HeightMeters,
                Velocity = goBody.velocity,                
                IsLanded = IsLanded,
            };
        }
    }
}
