using Assets.Source.Entities.GenericComponents;
using Assets.Source.Mvc.Models;
using DG.Tweening;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Components
{
    public class MotionBoot : AbstractPausableComponent<JesterEntity>
    {        
        private readonly PlayerModel _playerModel;
        private readonly FlightStatsModel _flightStatsmodel;
        private readonly UserInputModel _userInputModel;

        // ToDo [CONFIG] Move to config SO    
        private Vector3 direction = new Vector3(1, 1, 0);
        private const float forceChangeSeconds = 1.2f;

        private const float maxForceFactor = 1f;

        private IDisposable OnKickActionSubscription;
        private IDisposable OnJesterKickedSubscription;
        private Tweener kickForceTweener;


        public MotionBoot(JesterEntity owner, PlayerModel playerModel, FlightStatsModel flightStatsmodel, UserInputModel userInputModel)
            : base(owner)
        {            
            _playerModel = playerModel;
            _flightStatsmodel = flightStatsmodel;
            _userInputModel = userInputModel;

            kickForceTweener = DOTween
                .To((x) => _flightStatsmodel.RelativeKickForce.Value = x, 0, maxForceFactor, forceChangeSeconds)
                .SetLoops(-1, LoopType.Yoyo);

            OnKickActionSubscription = _userInputModel.OnClickedAnywhere
                .Subscribe(_ => OnKickUserAction())
                .AddTo(owner);

            OnJesterKickedSubscription = owner.OnKicked
                .Subscribe(_ => OnJesterKicked())
                .AddTo(owner);
        }

        private void OnKickUserAction()
        {
            OnKickActionSubscription?.Dispose();
            kickForceTweener?.Kill();
        }

        private void OnJesterKicked()
        {
            OnJesterKickedSubscription?.Dispose();

            Vector3 appliedForce = direction * (_playerModel.KickForce * _flightStatsmodel.RelativeKickForce.Value);
            Owner.GoBody.AddForce(appliedForce, ForceMode2D.Impulse);
        }
    }
}
