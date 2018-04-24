using Assets.Source.App;
using Assets.Source.Repositories;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester
{
    public class FlightRecorder : AbstractBodyBehaviour
    {        
        private JesterStateRepository jesterState;
        private Vector3 Origin;                        


        private void Start()
        {
            jesterState = App.Cache.JesterState;
            Origin = goTransform.position;            
        }


        private void LateUpdate()
        {
            jesterState.Distance = MathUtil.Difference(Origin.x, goTransform.position.x);
            jesterState.Height = MathUtil.Difference(Origin.y, goTransform.position.y);

            jesterState.Velocity = goBody.velocity;

            // Check if Flight has Started
            jesterState.IsStarted = !MathUtil.NearlyEqual(jesterState.Distance, 0);

            // Check if Jester has landed
            jesterState.IsLanded = jesterState.IsStarted && MathUtil.NearlyEqual(jesterState.Velocity.magnitude, 0);
        }
    }
}
