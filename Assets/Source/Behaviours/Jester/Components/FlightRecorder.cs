using Assets.Source.App;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class FlightRecorder : AbstractJesterComponent
    {        
        private readonly Vector3 origin;


        public FlightRecorder(Jester owner)
            : base(owner, false)
        {            
            origin = owner.goTransform.position;            

            // Is Started Flag
            App.Cache.RepoRx.JesterStateRepository.DistanceProperty
                                                  .Where(e => !e.IsNearlyEqual(0))
                                                  .Subscribe(_ => App.Cache.RepoRx.JesterStateRepository.IsStarted = true);                                      

            // Is Landed Flag
            App.Cache.RepoRx.JesterStateRepository.VelocityProperty
                                                  .Where(e => e.magnitude.IsNearlyEqual(0) && App.Cache.RepoRx.JesterStateRepository.IsStarted)
                                                  .Subscribe(_ => App.Cache.RepoRx.JesterStateRepository.IsLanded = true);
        }


        protected override void LateUpdate()
        {
            App.Cache.RepoRx.JesterStateRepository.Distance = origin.x.Difference(owner.goTransform.position.x);
            App.Cache.RepoRx.JesterStateRepository.Height = origin.y.Difference(owner.goTransform.position.y);

            App.Cache.RepoRx.JesterStateRepository.Velocity = owner.goBody.velocity;
        }
    }
}
