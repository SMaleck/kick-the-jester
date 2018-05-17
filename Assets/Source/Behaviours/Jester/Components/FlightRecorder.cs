using Assets.Source.App;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class FlightRecorder
    {
        private readonly AbstractBodyBehaviour owner;
        private readonly Vector3 origin;


        public FlightRecorder(AbstractBodyBehaviour owner)
        {
            this.owner = owner;            
            origin = owner.goTransform.position;

            Observable.EveryLateUpdate().Subscribe(_ => LateUpdate());

            // Is Started Flag
            App.Cache.RepoRx.JesterStateRepository.DistanceProperty
                                                  .Where(e => !e.IsNearlyEqual(0))
                                                  .Subscribe(_ => App.Cache.RepoRx.JesterStateRepository.IsStarted = true);                                      

            // Is Landed Flag
            App.Cache.RepoRx.JesterStateRepository.VelocityProperty
                                                  .Where(e => e.magnitude.IsNearlyEqual(0) && App.Cache.RepoRx.JesterStateRepository.IsStarted)
                                                  .Subscribe(_ => App.Cache.RepoRx.JesterStateRepository.IsLanded = true);
        }


        private void LateUpdate()
        {
            App.Cache.RepoRx.JesterStateRepository.Distance = origin.x.Difference(owner.goTransform.position.x);
            App.Cache.RepoRx.JesterStateRepository.Height = origin.y.Difference(owner.goTransform.position.y);

            App.Cache.RepoRx.JesterStateRepository.Velocity = owner.goBody.velocity;
        }
    }
}
