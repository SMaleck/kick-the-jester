using Assets.Source.App;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class FlightRecorder : AbstractComponent<JesterContainer>
    {        
        private readonly Vector3 origin;        


        public FlightRecorder(JesterContainer owner)
            : base(owner, false)
        {
            origin = owner.goTransform.position;

            // Set Jester IsStarted when he was kikced
            MessageBroker.Default.Receive<JesterEffects>()
                                 .Where(e => e.Equals(JesterEffects.Kick))
                                 .Subscribe(_ => owner.IsStartedProperty.Value = true)
                                 .AddTo(owner);

            // Listen to velocity, so we can update landed
            owner.VelocityProperty.Where(e => e.magnitude.IsNearlyEqual(0) && owner.Height.ToMeters() == 0 && owner.IsStartedProperty.Value)
                                  .Subscribe(__ => { owner.IsLandedProperty.Value = true; })
                                  .AddTo(owner);
        }


        protected override void LateUpdate()
        {
            owner.Distance = owner.goTransform.position.x.Difference(origin.x);
            owner.Height = owner.goTransform.position.y.Difference(origin.y);

            owner.Velocity = owner.goBody.velocity;
        }
    }
}
