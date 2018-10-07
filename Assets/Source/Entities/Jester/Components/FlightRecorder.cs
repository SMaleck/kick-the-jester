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
        private readonly ProfileModel _profileModel;

        private bool _canCheckForIsLanded = false;
        private bool _isLanded = false;


        public FlightRecorder(JesterEntity owner, FlightStatsModel flightStatsModel, ProfileModel profileModel)
            : base(owner)
        {
            origin = owner.GoTransform.position;
            _flightStatsModel = flightStatsModel;
            _profileModel = profileModel;

            owner.OnKicked
                .Subscribe(_ => _canCheckForIsLanded = true)
                .AddTo(owner);

            owner.OnLanded
                .Subscribe(_ => OnLanded())
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

        private void OnLanded()
        {
            var distance = _flightStatsModel.Distance.Value;
            var lastBestDistance = _profileModel.BestDistance.Value;

            _profileModel.BestDistance.Value = Mathf.Max(distance, lastBestDistance);
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