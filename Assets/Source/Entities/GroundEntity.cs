using Assets.Source.Entities.Cameras;
using Assets.Source.Entities.Jester;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Source.Entities
{
    public class GroundEntity : AbstractMonoEntity
    {
        private Transform _target;

        public override void Initialize()
        {
            Observable.EveryLateUpdate()
                .Subscribe(_ => OnLateUpdate())
                .AddTo(this);

        }

        [Inject]
        private void Inject(JesterEntity target, MainCamera camera)
        {
            _target = target.transform;
        }

        private void OnLateUpdate()
        {
            float nextX = _target.position.x;
            float nextY = transform.position.y;

            Position = new Vector3(nextX, nextY, Position.z);
        }
    }
}
