using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.Source.Entities.Jester.MonoComponents
{
    public class Collisions : AbstractMonoEntity
    {
        private const string TAG_GROUND = "Ground";
        private const string TAG_OBSTACLE = "Obstacle";
        private const string TAG_BOOST = "Boost";
        private const string TAG_PICKUP = "Pickup";

        [SerializeField] private Collider2D _groundCollisionProbe;
        [SerializeField] private Collider2D _radialCollisionProbe;

        private Subject<Unit> _onGround = new Subject<Unit>();
        public IObservable<Unit> OnGround => _onGround;

        private Subject<Unit> _onBoost = new Subject<Unit>();
        public IObservable<Unit> OnBoost => _onBoost;

        private Subject<Unit> _onPickup = new Subject<Unit>();
        public IObservable<Unit> OnPickup => _onPickup;

        private Subject<Unit> _onObstacle = new Subject<Unit>();
        public IObservable<Unit> OnObstacle => _onObstacle;

        public override void Initialize()
        {
            _onGround.AddTo(Disposer);
            _onBoost.AddTo(Disposer);
            _onPickup.AddTo(Disposer);
            _onObstacle.AddTo(Disposer);

            _groundCollisionProbe.OnTriggerEnter2DAsObservable()
                .Subscribe(OnGroundCollisionProbeEntered)
                .AddTo(Disposer);

            _radialCollisionProbe.OnTriggerEnter2DAsObservable()
                .Subscribe(OnRadialCollisionProbeEntered)
                .AddTo(Disposer);
        }

        private void OnGroundCollisionProbeEntered(Collider2D collider)
        {
            if (collider.gameObject.tag == TAG_GROUND)
            {
                _onGround.OnNext(Unit.Default);
            }
        }

        private void OnRadialCollisionProbeEntered(Collider2D collider)
        {
            switch (collider.gameObject.tag)
            {
                case TAG_OBSTACLE:
                    _onObstacle.OnNext(Unit.Default);
                    break;

                case TAG_BOOST:
                    _onBoost.OnNext(Unit.Default);
                    break;

                case TAG_PICKUP:
                    _onPickup.OnNext(Unit.Default);
                    break;
            }
        }
    }
}
