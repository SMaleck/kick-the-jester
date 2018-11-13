using Assets.Source.Entities.Jester;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Source.Entities
{
    public class SkyEntity : AbstractMonoEntity
    {
        private JesterEntity _jesterEntity;
        private Vector3 _origin;
        private float xOffset;
        
        [SerializeField] private Transform _moon;

        [Range(0f, 10f)]
        [SerializeField] private float _verticalMoonWobble = 0.8f;
        [SerializeField] private float _moonWobbleSpeedSeconds = 3;

        [Inject]
        private void Inject(JesterEntity jesterEntity)
        {
            _jesterEntity = jesterEntity;
        }

        public override void Initialize()
        {
            _origin = Position;
            xOffset = Position.x - _jesterEntity.Position.x;

            Observable.EveryLateUpdate()
                .Subscribe(_ => OnLateUpdate())
                .AddTo(this);

            var targetVector = _moon.localPosition + (Vector3.up * _verticalMoonWobble);
            _moon.DOLocalMove(targetVector, _moonWobbleSpeedSeconds)
                .SetEase(Ease.InOutCubic)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnLateUpdate()
        {
            FollowHorizontal();
        }

        private void FollowHorizontal()
        {
            var xPos = _jesterEntity.Position.x + xOffset;

            Position = new Vector3(xPos, Position.y, Position.z);
        }
    }
}
