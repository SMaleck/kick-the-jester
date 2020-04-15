using Assets.Source.Entities.Jester;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Source.Entities
{
    public class SpawnAnchorEntity : AbstractMonoEntity
    {
        private JesterEntity _jesterEntity;

        protected float OffsetX;

        [Inject]
        private void Inject(JesterEntity jesterEntity)
        {
            _jesterEntity = jesterEntity;

            OffsetX = Position.x - _jesterEntity.Position.x;
        }

        public override void Initialize()
        {
            Observable.EveryLateUpdate()
                .Subscribe(_ => OnLateUpdate())
                .AddTo(this);
        }

        private void OnLateUpdate()
        {
            Position = new Vector3(
                _jesterEntity.Position.x + OffsetX,
                Position.y,
                Position.z);
        }
    }
}
