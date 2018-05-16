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
            jesterState = App.Cache.RepoRx.JesterStateRepository;
            Origin = goTransform.position;            
        }


        private void LateUpdate()
        {
            jesterState.Distance = Origin.x.Difference(goTransform.position.x);
            jesterState.Height = Origin.y.Difference(goTransform.position.y);

            jesterState.Velocity = goBody.velocity;

            // Check if Flight has Started
            jesterState.IsStarted = !jesterState.Distance.IsNearlyEqual(0);

            // Check if Jester has landed
            jesterState.IsLanded = jesterState.IsStarted && jesterState.Velocity.magnitude.IsNearlyEqual(0);
        }
    }
}
