using Assets.Source.Entities.GenericComponents;
using Assets.Source.Mvc.Models;
using Assets.Source.Util;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Components
{
    public class VelocityLimiter : AbstractPausableComponent<JesterEntity>
    {
        private readonly PlayerModel _playerModel;
        private readonly FlightStatsModel _flightStatsModel;

        private float velocityX
        {
            get { return Owner.GoBody.velocity.x; }
        }

        private float velocityY
        {
            get { return Owner.GoBody.velocity.y; }
        }


        public VelocityLimiter(JesterEntity owner, PlayerModel playerModel, FlightStatsModel flightStatsModel) : 
            base(owner)
        {
            _playerModel = playerModel;
            _flightStatsModel = flightStatsModel;

            Observable.EveryLateUpdate()
                .Subscribe(_ => OnLateUpdate())
                .AddTo(owner);
        }


        private void OnLateUpdate()
        {
            float clampedX = Mathf.Clamp(velocityX, 0, _playerModel.MaxVelocityX);
            float clampedY = Mathf.Clamp(velocityY, - float.MaxValue, _playerModel.MaxVelocityY);

            Owner.GoBody.velocity = new Vector2(clampedX, clampedY);

            float relativeVelocity = velocityX.AsRelativeTo(_playerModel.MaxVelocityX);
            _flightStatsModel.RelativeVelocity.Value = relativeVelocity;
        }
    }
}
