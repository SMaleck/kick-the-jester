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
        private event UnitEventHandler _OnDistanceChanged = delegate { };
        public void OnDistanceChanged(UnitEventHandler handler)
        {
            _OnDistanceChanged += handler;
        }

        private float distance = 0;
        public float Distance
        {
            get { return distance; }
            private set
            {
                if (distance != value)
                {
                    distance = value;
                    _OnDistanceChanged(new UnitMeasurement(distance));
                }
            }
        }


        // HEIGHT
        private event UnitEventHandler _OnHeightChanged = delegate { };
        public void OnHeightChanged(UnitEventHandler handler)
        {
            _OnHeightChanged += handler;            
        }

        private float height = 0;
        public float Height
        {
            get { return height; }
            private set
            {
                if(height != value)
                {
                    height = value;
                    _OnHeightChanged(new UnitMeasurement(height));
                }
            }
        }


        // VELOCITY
        private event SpeedEventHandler _OnVelocityChanged = delegate { };
        public void OnVelocityChanged(SpeedEventHandler handler)
        {
            _OnVelocityChanged += handler;
        }

        private Vector3 velocity = new Vector3();
        public Vector3 Velocity
        {
            get { return velocity; }
            private set
            {
                if (velocity != value)
                {
                    velocity = value;
                    _OnVelocityChanged(new SpeedMeasurement(velocity));
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
            Distance = MathUtil.Difference(Origin.x, goTransform.position.x);
            Height = MathUtil.Difference(Origin.y, goTransform.position.y);            

            Velocity = goBody.velocity;            

            // Check if Flight has Started
            IsStared = !MathUtil.NearlyEqual(Distance, 0);

            // Check if Jester has landed
            IsLanded = IsStared && MathUtil.NearlyEqual(Velocity.magnitude, 0);
        }
    }
}
