using Assets.Source.Entities.Jester;
using Assets.Source.Mvc.Models;
using UniRx;
using UnityEngine;
using Zenject;
using DG.Tweening;
using Assets.Source.App.Configuration;

namespace Assets.Source.Entities.Cameras
{
    public class MainCamera : AbstractMonoEntity
    {
        [SerializeField] private Camera _camera;
        public Camera Camera => _camera;

        private CameraConfig _config;
        private JesterEntity _jester;
        private FlightStatsModel _flighStatsModel;
        private Vector3 _origin;

        private bool _shouldFollow = true;
        private float OffsetX => _config.OffsetX;
        private float OvertakeOffsetX => _config.OvertakeOffsetX;
        private float OvertakeSeconds => _config.OvertakeSeconds;

        private bool _isShaking = false;
        private float RelativeVelocityThreshold => _config.RelativeVelocityThresholdForShake;
        private float ShakeSeconds => _config.ShakeSeconds;


        private float _cameraWidth;
        public float Width
        {
            get
            {
                if (_cameraWidth <= 0)
                {                
                    _cameraWidth = Height * _camera.aspect;
                }

                return _cameraWidth;
            }
        }

        private float _cameraHeight;
        public float Height
        {
            get
            {
                if (_cameraHeight <= 0)
                {
                    _cameraHeight = 2f * _camera.orthographicSize;
                }

                return _cameraHeight;
            }
        }


        [Inject]
        private void Inject(CameraConfig config, JesterEntity jester, FlightStatsModel flighStatsModel)
        {
            _config = config;
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
            GoTransform.DOShakePosition(ShakeSeconds, _flighStatsModel.RelativeVelocity.Value, 8)
                .OnComplete(ResetShake);
        }


        // ToDo Smoothly reset shake
        private void ResetShake()
        {
            _isShaking = false;
            Position = new Vector3(Position.x, GetTargetY(), Position.z);
        }
    }
}
