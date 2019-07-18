using Assets.Source.App.Configuration;
using Assets.Source.Entities.GenericComponents;
using Assets.Source.Features.PlayerData;
using Assets.Source.Mvc.Models;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Components
{
    public class MotionShoot : AbstractPausableComponent<JesterEntity>
    {
        private readonly ShootConfig _shootConfig;
        private readonly PlayerAttributesModel _playerAttributesModel;
        private readonly FlightStatsModel _flightStatsModel;

        private bool _isInFlight;
        private bool _hasProjectiles => _flightStatsModel.ShotsRemaining.Value > 0;

        public MotionShoot(
            JesterEntity owner,
            ShootConfig shootConfig,
            PlayerAttributesModel playerAttributesModel,
            FlightStatsModel flightStatsModel,
            UserInputModel userInputModel)
            : base(owner)
        {
            _shootConfig = shootConfig;
            _playerAttributesModel = playerAttributesModel;
            _flightStatsModel = flightStatsModel;

            _playerAttributesModel.ProjectileCount
                .Subscribe(flightStatsModel.SetRemainingShotsIfHigher)
                .AddTo(Disposer);            

            userInputModel.OnClickedAnywhere
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
            if (!_hasProjectiles || !_isInFlight) { return; }

            Owner.OnShot.Execute();
            AdjustVerticalVelocity();
            Vector3 appliedForce = _shootConfig.ForceDirection * _playerAttributesModel.ShootForce.Value;
            Owner.GoBody.AddForce(appliedForce, ForceMode2D.Impulse);
            
            _flightStatsModel.ShotsRemaining.Value--;
        }

        private void AdjustVerticalVelocity()
        {
            var currentVelocity = Owner.GoBody.velocity;

            if (currentVelocity.y > 0)
            {
                return;
            }

            var adjustedVerticalVelocity = currentVelocity.y * _shootConfig.VerticalVelocityReductionFactor;
            Owner.GoBody.velocity = new Vector2(currentVelocity.x, adjustedVerticalVelocity);
        }
    }
}
