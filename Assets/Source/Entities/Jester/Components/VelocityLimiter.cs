using Assets.Source.Entities.GenericComponents;
using Assets.Source.Features.PlayerData;
using Assets.Source.Mvc.Models;
using Assets.Source.Util;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Components
{
    public class VelocityLimiter : AbstractPausableComponent<JesterEntity>
    {
        private readonly PlayerAttributesModel _playerAttributesModel;
        private readonly FlightStatsModel _flightStatsModel;

        private float velocityX
        {
            get { return Owner.GoBody.velocity.x; }
        }

        private float velocityY
        {
            get { return Owner.GoBody.velocity.y; }
        }


        public VelocityLimiter(JesterEntity owner, PlayerAttributesModel playerAttributesModel, FlightStatsModel flightStatsModel) : 
            base(owner)
        {
            _playerAttributesModel = playerAttributesModel;
            _flightStatsModel = flightStatsModel;

            Observable.EveryLateUpdate()
                .Subscribe(_ => OnLateUpdate())
                .AddTo(owner);
        }


        private void OnLateUpdate()
        {
            float clampedX = Mathf.Clamp(velocityX, 0, _playerAttributesModel.MaxVelocityX);
            float clampedY = Mathf.Clamp(velocityY, - float.MaxValue, _playerAttributesModel.MaxVelocityY);

            Owner.GoBody.velocity = new Vector2(clampedX, clampedY);

            float relativeVelocity = velocityX.AsRelativeTo(_playerAttributesModel.MaxVelocityX);
            _flightStatsModel.RelativeVelocity.Value = relativeVelocity;
        }
    }
}
