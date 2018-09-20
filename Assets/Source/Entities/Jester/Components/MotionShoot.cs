using Assets.Source.Entities.GenericComponents;
using Assets.Source.Mvc.Models;
using Assets.Source.Services;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Components
{
    public class MotionShoot : AbstractPausableComponent<JesterEntity>
    {
        private readonly PlayerModel _playerModel;
        private readonly FlightStatsModel _flightStatsmodel;
        private readonly UserControlService _userControlService;

        private bool _isActive;
        private bool _isInFlight;
        private Vector3 _direction = new Vector3(1.2f, 1, 0);


        public MotionShoot(JesterEntity owner, PlayerModel playerModel, FlightStatsModel flightStatsmodel, UserControlService userControlService) 
            : base(owner)
        {
            _playerModel = playerModel;
            _flightStatsmodel = flightStatsmodel;
            _userControlService = userControlService;

            flightStatsmodel.ShotsRemaining.Value = playerModel.Shots;

            userControlService.OnKick                
                .Subscribe(_ => OnKick())
                .AddTo(owner);                       

            _flightStatsmodel.IsStarted   
                .CombineLatest(_flightStatsmodel.IsLanded, (started, landed) => started && !landed)
                .Subscribe(isInFlight => _isInFlight = isInFlight)
                .AddTo(owner);
        }        


        private void OnKick()
        {
            if (!_isActive || !_isInFlight) { return; }
            
            owner.OnShot.Execute();

            Vector3 appliedForce = _direction * _playerModel.ShootForce;
            owner.GoBody.AddForce(appliedForce, ForceMode2D.Impulse);

            _flightStatsmodel.ShotsRemaining.Value--;
            _isActive = _flightStatsmodel.ShotsRemaining.Value <= 0;
        }
    }
}
