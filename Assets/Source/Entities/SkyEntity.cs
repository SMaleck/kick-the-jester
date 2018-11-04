using Assets.Source.Entities.Jester;
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
        }

        private void OnLateUpdate()
        {
            if (_jesterEntity.Position.y > _origin.y)
            {
                FollowBoth();
                return;
            }

            FollowHorizontal();
        }

        private void FollowBoth()
        {
            var xPos = _jesterEntity.Position.x + xOffset;
            var yPos = _jesterEntity.Position.y;

            Position = new Vector3(xPos, yPos, Position.z);
        }

        private void FollowHorizontal()
        {
            var xPos = _jesterEntity.Position.x + xOffset;

            Position = new Vector3(xPos, Position.y, Position.z);
        }
    }
}
