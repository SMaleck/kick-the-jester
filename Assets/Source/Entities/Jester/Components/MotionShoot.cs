using Assets.Source.Entities.GenericComponents;
using Assets.Source.Features.PlayerData;
using Assets.Source.Mvc.Models;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Components
{
    public class MotionShoot : AbstractPausableComponent<JesterEntity>
    {
        private readonly PlayerAttributesModel _playerAttributesModel;
        private readonly FlightStatsModel _flightStatsModel;
        private readonly UserInputModel _userInputModel;

        private bool _isActive = true;
        private bool _isInFlight;
        private Vector3 _direction = new Vector3(1.2f, 1, 0);


        public MotionShoot(
            JesterEntity owner,
            PlayerAttributesModel playerAttributesModel,
            FlightStatsModel flightStatsModel,
            UserInputModel userInputModel)
            : base(owner)
        {
            _playerAttributesModel = playerAttributesModel;
            _flightStatsModel = flightStatsModel;
            _userInputModel = userInputModel;

            flightStatsModel.ShotsRemaining.Value = _playerAttributesModel.Shots;

            _userInputModel.OnClickedAnywhere
                .Subscribe(_ => OnKick())
                .AddTo(owner);

            owner.OnKicked
                .Subscribe(_ => _isInFlight = true)
                .AddTo(owner);

            owner.OnLanded
                .Subscribe(_ => _isInFlight = false)
                .AddTo(owner);
        }


        private void OnKick()
        {
            if (!_isActive || !_isInFlight) { return; }

            Owner.OnShot.Execute();

            Vector3 appliedForce = _direction * _playerAttributesModel.ShootForce;
            Owner.GoBody.AddForce(appliedForce, ForceMode2D.Impulse);

            _flightStatsModel.ShotsRemaining.Value--;
            _isActive = _flightStatsModel.ShotsRemaining.Value > 0;
        }
    }
}
