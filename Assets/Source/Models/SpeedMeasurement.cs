using Assets.Source.App;
using UnityEngine;

namespace Assets.Source.Models
{
    public class SpeedMeasurement
    {
        public Vector3 Vector;
        public float Kmh
        {
            get { return MathUtil.CappedFloat(Vector.magnitude); }
        }

        public SpeedMeasurement(Vector3 Vector = new Vector3())
        {
            this.Vector = Vector;
        }
    }
}
