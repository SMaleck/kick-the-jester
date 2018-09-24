using Assets.Source.Entities.GenericComponents;
using Assets.Source.Mvc.Models;
using Assets.Source.Services;
using DG.Tweening;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Components
{
    public class MotionBoot : AbstractPausableComponent<JesterEntity>
    {
        private readonly UserControlService _userControlService;
        private readonly PlayerModel _playerModel;
        private readonly FlightStatsModel _flightStatsmodel;

        private bool isActive = true;         
        private Vector3 direction = new Vector3(1, 1, 0);

        private const float forceChangeSeconds = 1.2f;
        private const float maxForceFactor = 1f;        

        private Tweener kickForceTweener;


        public MotionBoot(JesterEntity owner, PlayerModel playerModel, FlightStatsModel flightStatsmodel, UserControlService userControlService)
            : base(owner)
        {            
            _userControlService = userControlService;
            _playerModel = playerModel;
            _flightStatsmodel = flightStatsmodel;

            owner.OnKicked
                .Where(_ => isActive)
                .Subscribe(_ => OnKick())
                .AddTo(owner);            

            flightStatsmodel.RelativeKickForce.Value = 0;

            kickForceTweener = DOTween
                .To(() => _flightStatsmodel.RelativeKickForce.Value, (x) => _flightStatsmodel.RelativeKickForce.Value = x, maxForceFactor, forceChangeSeconds)                
                .SetLoops(-1, LoopType.Yoyo);

            isActive = true;
        }


        private void OnKick()
        {           
            Vector3 appliedForce = direction * (_playerModel.KickForce * _flightStatsmodel.RelativeKickForce.Value);
            Owner.GoBody.AddForce(appliedForce, ForceMode2D.Impulse);

            kickForceTweener.Kill();
            isActive = false;
        }
    }
}
