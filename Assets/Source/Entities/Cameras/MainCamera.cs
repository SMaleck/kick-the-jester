using Assets.Source.Entities.Jester;
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
        private Vector3 _origin;
                
        private bool _shouldFollow = true;
        private const float OffsetX = 3.5f;
        private const float OvertakeOffsetX = 5.5f;
        private const float OvertakeSeconds = 0.8f;

        private const float ShakeSeconds = 0.1f;

        [Inject]
        private void Inject(JesterEntity jester)
        {
            _jester = jester;            
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
            if (!_shouldFollow) { return; }

            Vector3 targetPos = _jester.Position;
            Vector3 currentPos = Position;

            Position = new Vector3(targetPos.x + OffsetX, Mathf.Clamp(targetPos.y, _origin.y, float.MaxValue), currentPos.z);
        }


        private void OnLanded()
        {
            _shouldFollow = false;
            GoTransform.DOMoveX(_jester.Position.x + OvertakeOffsetX, OvertakeSeconds);
        }

        
        public void Shake()
        {
            // ToDo only shake when velocity Y is above a threshhold
            GoTransform.DOShakePosition(ShakeSeconds);
        }
    }
}
