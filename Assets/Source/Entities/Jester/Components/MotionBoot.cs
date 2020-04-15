using Assets.Source.App.Configuration;
using Assets.Source.Entities.GenericComponents;
using Assets.Source.Features.PlayerData;
using Assets.Source.Mvc.Models;
using Assets.Source.Util;
using DG.Tweening;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Components
{
    public class MotionBoot : AbstractPausableComponent<JesterEntity>
    {
        private readonly PlayerAttributesModel _playerAttributesModel;
        private readonly GameRoundStatsModel _gameRoundStatsModel;
        private readonly UserInputModel _userInputModel;
        private readonly BootConfig _bootConfig;

        private readonly IDisposable OnKickActionSubscription;
        private readonly IDisposable OnJesterKickedSubscription;
        private readonly Tweener _kickForceTweener;

        private bool _wasKicked = false;

        public MotionBoot(
            JesterEntity owner,
            PlayerAttributesModel playerAttributesModel,
            GameRoundStatsModel gameRoundStatsModel,
            UserInputModel userInputModel,
            BootConfig bootConfig)
            : base(owner)
        {
            _playerAttributesModel = playerAttributesModel;
            _gameRoundStatsModel = gameRoundStatsModel;
            _userInputModel = userInputModel;
            _bootConfig = bootConfig;

            OnKickActionSubscription = _userInputModel.OnClickedAnywhere
                .Subscribe(_ => OnKickUserAction())
                .AddTo(owner);

            OnJesterKickedSubscription = owner.OnKicked
                .Subscribe(_ => OnJesterKicked())
                .AddTo(owner);

            _kickForceTweener = DOTween
                .To(
                _gameRoundStatsModel.SetRelativeKickForce,
                _bootConfig.MinForceFactor,
                _bootConfig.MaxForceFactor,
                _bootConfig.ForceFactorChangeSeconds)
                .SetLoops(-1, LoopType.Yoyo)
                .SetAutoKill(false);

            Owner.IsPaused
                .Where(_ => !_wasKicked)
                .Subscribe(isPaused =>
                {
                    if (isPaused && _kickForceTweener.IsPlaying())
                    {
                        _kickForceTweener.Pause();
                    }
                    else if (!_kickForceTweener.IsPlaying())
                    {
                        _kickForceTweener.Play();
                    }
                })
                .AddTo(owner);
        }

        private void OnKickUserAction()
        {
            OnKickActionSubscription?.Dispose();
            _kickForceTweener?.Pause();
        }

        private void OnJesterKicked()
        {
            _wasKicked = true;
            OnJesterKickedSubscription?.Dispose();

            var kickForce = _playerAttributesModel.KickForce.Value;
            Vector3 appliedForce = _bootConfig.ForceDirection * (kickForce * _gameRoundStatsModel.RelativeKickForce.Value);
            Owner.GoBody.AddForce(appliedForce, ForceMode2D.Impulse);
        }
    }
}
