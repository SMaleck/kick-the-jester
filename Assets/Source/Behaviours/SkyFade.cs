using Assets.Source.App;
using Assets.Source.Behaviours.Jester;
using Assets.Source.Models;
using UnityEngine;

namespace Assets.Source.Behaviours
{
    public class SkyFade : AbstractBehaviour
    {
        public float maxY = 15;
        public float minY = -5;
        public float maxHeight = 200;        


        public void Start()
        {            
            App.Cache.jester.GetComponent<FlightRecorder>().OnHeightChanged(MoveSkyFade);
        }


        public void MoveSkyFade(UnitMeasurement height)
        {
            float heightPercent = height.Units / maxHeight;           
            float yRange = MathUtil.Difference(minY, maxY);
            
            float nextY = maxY - (heightPercent * yRange);            

            goTransform.localPosition = new Vector3(goTransform.localPosition.x, Mathf.Max(minY, nextY), goTransform.localPosition.z);            
        }
    }
}
