using Assets.Source.Entities.GenericComponents;
using Assets.Source.Mvc.Models;
using Assets.Source.Util;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Components
{
    public class FlightRecorder : AbstractPausableComponent<JesterEntity>
    {        
        private readonly Vector3 origin;
        private readonly FlightStatsModel _flightStatsModel;

        public FlightRecorder(JesterEntity owner, FlightStatsModel flightStatsModel)
            : base(owner)
        {
            origin = owner.GoTransform.position;
            _flightStatsModel = flightStatsModel;

            // TODO Unsafe, change to global event from Knight
            _flightStatsModel.Velocity
                .Where(_ => !_flightStatsModel.IsStarted.Value && !IsPaused.Value)
                .Subscribe(OnVelocityChangedAtLaunch)
                .AddTo(owner);

            _flightStatsModel.Velocity
                .Where(_ => _flightStatsModel.IsStarted.Value && !IsPaused.Value)
                .Subscribe(OnVelocityChangedAfterStart)
                .AddTo(owner);

            Observable.EveryLateUpdate()
                .Where(_ => !IsPaused.Value)
                .Subscribe(_ => LateUpdate())
                .AddTo(owner);
        }


        private void LateUpdate()
        {
            _flightStatsModel.Distance.Value = owner.GoTransform.position.x.Difference(origin.x);
            _flightStatsModel.Height.Value = owner.GoTransform.position.y.Difference(origin.y);

            _flightStatsModel.Velocity.Value = owner.GoBody.velocity;
        }

        private void OnVelocityChangedAtLaunch(Vector2 velocity)
        {
            bool isMoving = velocity.magnitude > 0;
            _flightStatsModel.IsStarted.Value = isMoving;
        }

        private void OnVelocityChangedAfterStart(Vector2 velocity)
        {
            bool isOnGround = _flightStatsModel.Height.Value.ToMeters() == 0;
            bool isStopped = velocity.magnitude.IsNearlyEqual(0);

            _flightStatsModel.IsLanded.Value = isOnGround && isStopped;
        }
    }
}