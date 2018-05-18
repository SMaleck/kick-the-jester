using Assets.Source.App;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class FlightRecorder : AbstractComponent<Jester>
    {        
        private readonly Vector3 origin;
        private bool jesterIsStarted = false;


        public FlightRecorder(Jester owner)
            : base(owner, false)
        {            
            origin = owner.goTransform.position;

            owner.OnStarted = owner.DistanceProperty.Select(e => !e.IsNearlyEqual(0)).ToReactiveCommand();
            owner.OnStarted.Subscribe(_ => jesterIsStarted = true).AddTo(owner);

            owner.OnLanded = owner.VelocityProperty.Select(e => e.magnitude.IsNearlyEqual(0) && jesterIsStarted).ToReactiveCommand();
        }


        protected override void LateUpdate()
        {
            owner.Distance = origin.x.Difference(owner.goTransform.position.x);
            owner.Height = origin.y.Difference(owner.goTransform.position.y);

            owner.Velocity = owner.goBody.velocity;
        }
    }
}
