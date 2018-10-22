using Assets.Source.App.Configuration;
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
        private readonly BootConfig _bootConfig;

        private readonly IDisposable OnKickActionSubscription;
        private readonly IDisposable OnJesterKickedSubscription;
        private readonly Tweener _kickForceTweener;


        public MotionBoot(
            JesterEntity owner,
            PlayerModel playerModel,
            FlightStatsModel flightStatsmodel,
            UserInputModel userInputModel,
            BootConfig bootConfig)
            : base(owner)
        {
            _playerModel = playerModel;
            _flightStatsmodel = flightStatsmodel;
            _userInputModel = userInputModel;
            _bootConfig = bootConfig;

            OnKickActionSubscription = _userInputModel.OnClickedAnywhere
                .Subscribe(_ => OnKickUserAction())
                .AddTo(owner);

            OnJesterKickedSubscription = owner.OnKicked
                .Subscribe(_ => OnJesterKicked())
                .AddTo(owner);

            _kickForceTweener = DOTween
                .To((x) => _flightStatsmodel.RelativeKickForce.Value = x, _bootConfig.MinForceFactor, _bootConfig.MaxForceFactor, _bootConfig.ForceFactorChangeSeconds)
                .SetLoops(-1, LoopType.Yoyo);

            Owner.IsPaused.Subscribe(isPaused =>
            {
                if (isPaused && _kickForceTweener.IsPlaying())
                {
                    _kickForceTweener?.Pause();
                }
                else if (!_kickForceTweener.IsPlaying())
                {
                    _kickForceTweener?.Play();
                }
            })
            .AddTo(owner);
        }

        private void OnKickUserAction()
        {
            OnKickActionSubscription?.Dispose();
            _kickForceTweener?.Kill();
        }

        private void OnJesterKicked()
        {
            OnJesterKickedSubscription?.Dispose();

            Vector3 appliedForce = _bootConfig.ForceDirection * (_playerModel.KickForce * _flightStatsmodel.RelativeKickForce.Value);
            Owner.GoBody.AddForce(appliedForce, ForceMode2D.Impulse);
        }
    }
}
