using Assets.Source.Entities.Jester;
using Assets.Source.Mvc.Models;
using UniRx;
using UnityEngine;
using Zenject;
using DG.Tweening;

namespace Assets.Source.Entities.Cameras
{
    public class MainCamera : AbstractMonoEntity
    {
        [SerializeField] private Camera _camera;

        private JesterEntity _jester;
        private FlightStatsModel _flighStatsModel;
        private Vector3 _origin;

        private bool _shouldFollow = true;
        private const float OffsetX = 3.5f;
        private const float OvertakeOffsetX = 5.5f;
        private const float OvertakeSeconds = 0.8f;

        private bool _isShaking = false;
        private const float RelativeVelocityThreshold = 0.2f;
        private const float ShakeSeconds = 0.1f;

        [Inject]
        private void Inject(JesterEntity jester, FlightStatsModel flighStatsModel)
        {
            _jester = jester;
            _flighStatsModel = flighStatsModel;
            _origin = transform.position;
        }

        public override void Initialize()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => OnUpdate())
                .AddTo(this);

            _jester.OnLanded
                .Subscribe(_ => OnLanded())
                .AddTo(this);

            _jester.Collisions
                .OnGround
                .Subscribe(_ => Shake())
                .AddTo(this);
        }


        private void OnUpdate()
        {
            if (!_shouldFollow || _isShaking) { return; }

            Vector3 targetPos = _jester.Position;

            Position = new Vector3(targetPos.x + OffsetX, GetTargetY(), Position.z);
        }

        private float GetTargetY()
        {
            return Mathf.Clamp(_jester.Position.y, _origin.y, float.MaxValue);
        }

        private void OnLanded()
        {
            _shouldFollow = false;
            GoTransform.DOMoveX(_jester.Position.x + OvertakeOffsetX, OvertakeSeconds);
        }


        private void Shake()
        {
            if (_flighStatsModel.RelativeVelocity.Value <= RelativeVelocityThreshold)
            {
                return;
            }

            _isShaking = true;
            GoTransform.DOShakePosition(ShakeSeconds, _flighStatsModel.RelativeVelocity.Value)
                .OnComplete(ResetShake);
        }

        private void ResetShake()
        {
            _isShaking = false;
            Position = new Vector3(Position.x, GetTargetY(), Position.z);
        }
    }
}
