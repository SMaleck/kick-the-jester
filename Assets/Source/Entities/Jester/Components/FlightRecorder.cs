using Assets.Source.Entities.GenericComponents;
using Assets.Source.Features.PlayerData;
using Assets.Source.Util;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Components
{
    public class FlightRecorder : AbstractPausableComponent<JesterEntity>
    {
        private readonly Vector3 origin;
        private readonly FlightStatsModel _flightStatsModel;

        private bool _canCheckForIsLanded = false;
        private bool _isLanded = false;


        public FlightRecorder(JesterEntity owner, FlightStatsModel flightStatsModel)
            : base(owner)
        {
            origin = owner.GoTransform.position;
            _flightStatsModel = flightStatsModel;

            owner.OnKicked
                .Subscribe(_ => _canCheckForIsLanded = true)
                .AddTo(owner);

            Observable.EveryLateUpdate()
                .Where(_ => !IsPaused.Value)
                .Subscribe(_ => LateUpdate())
                .AddTo(owner);
        }


        private void LateUpdate()
        {
            _flightStatsModel.Distance.Value = Owner.GoTransform.position.x.Difference(origin.x);
            _flightStatsModel.Height.Value = Owner.GoTransform.position.y.Difference(origin.y);

            _flightStatsModel.Velocity.Value = Owner.GoBody.velocity;

            CheckIsLanded();
        }

        private void CheckIsLanded()
        {
            if (!_canCheckForIsLanded || _isLanded) { return; }

            bool isOnGround = _flightStatsModel.Height.Value.ToMeters() <= 0;
            bool isStopped = _flightStatsModel.Velocity.Value.magnitude.IsNearlyEqual(0);

            _isLanded = isOnGround && isStopped;

            if (_isLanded)
            {
                Owner.OnLanded.Execute();
            }
        }
    }
}