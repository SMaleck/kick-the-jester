using Assets.Source.Util;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Source.Features.PlayerData
{
    public class FlightStatsModel : AbstractDisposable
    {
        private readonly ReactiveProperty<Vector3> _origin;
        public IReadOnlyReactiveProperty<Vector3> Origin => _origin;

        private readonly ReactiveProperty<Vector3> _position;
        public IReadOnlyReactiveProperty<Vector3> Position => _position;

        private readonly ReactiveProperty<Vector2> _velocity;
        public IReadOnlyReactiveProperty<Vector2> Velocity => _velocity;

        private readonly ReactiveProperty<float> _relativeVelocity;
        public IReadOnlyReactiveProperty<float> RelativeVelocity => _relativeVelocity;

        public readonly IReadOnlyReactiveProperty<float> DistanceUnits;
        public readonly IReadOnlyReactiveProperty<float> HeightUnits;
        public readonly IReadOnlyReactiveProperty<float> MaxHeightReached;
        public readonly IReadOnlyReactiveProperty<bool> IsLanded;

        public FlightStatsModel()
        {
            _origin = new ReactiveProperty<Vector3>().AddTo(Disposer);
            _position = new ReactiveProperty<Vector3>().AddTo(Disposer);
            _velocity = new ReactiveProperty<Vector2>().AddTo(Disposer);
            _relativeVelocity = new ReactiveProperty<float>().AddTo(Disposer);

            DistanceUnits = _position
                .Select(position => position.x.Difference(Origin.Value.x))
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposer);

            HeightUnits = _position
                .Select(position => position.y.Difference(Origin.Value.y))
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposer);

            MaxHeightReached = HeightUnits
                .Select(height => Mathf.Max(height, MaxHeightReached.Value))
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposer);

            IsLanded = Observable.Merge(
                    HeightUnits.AsUnitObservable(),
                    Velocity.AsUnitObservable())
                .Select(_ =>
                {
                    var isOnGround = HeightUnits.Value.ToMeters() <= 0;
                    var isStopped = Velocity.Value.magnitude.IsNearlyEqual(0);
                    return isOnGround && isStopped;
                })
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposer);
        }

        public void SetOrigin(Vector3 value)
        {
            _origin.Value = value;
        }

        public void SetPosition(Vector3 value)
        {
            _position.Value = value;
        }

        public void SetVelocity(Vector2 value)
        {
            _velocity.Value = value;
        }

        public void SetRelativeVelocity(float value)
        {
            _relativeVelocity.Value = value;
        }
    }
}
