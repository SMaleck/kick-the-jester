using Assets.Source.App;
using System;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Components
{
    class FlightDistance : MonoBehaviour
    {
        private Transform Target;
        private Vector3 Origin;

        private float DistanceUnits;
        private float DistanceMeters;

        public void Start()
        {
            Target = Singletons.jester.GetTransform();
            Origin = Target.position;
        }

        public void LateUpdate()
        {
            DistanceUnits = Math.Abs(Target.position.x) - Math.Abs(Origin.x);
            DistanceMeters = DistanceUnits * Constants.UnitToMeterFactor;

            Singletons.uiManager.UpdateDistance(DistanceMeters);
        }
    }
}
