using System.Linq;
using Assets.Source.Util;
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

        public readonly IReadOnlyReactiveProperty<float> Distance;
        public readonly IReadOnlyReactiveProperty<float> Height;
        public readonly IReadOnlyReactiveProperty<float> MaxHeightReached;
        public readonly IReadOnlyReactiveProperty<bool> IsLanded;

        // ToDo Extract to separate Model
        public FloatReactiveProperty RelativeKickForce;
        public FloatReactiveProperty RelativeVelocity;
        public IntReactiveProperty ShotsRemaining;

        public ReactiveCollection<int> Gains;
        public IReadOnlyReactiveProperty<int> TotalCollectedCurrency;

        public FlightStatsModel()
        {
            RelativeKickForce = new FloatReactiveProperty().AddTo(Disposer);
            RelativeVelocity = new FloatReactiveProperty().AddTo(Disposer);
            ShotsRemaining = new IntReactiveProperty().AddTo(Disposer);
            Gains = new ReactiveCollection<int>().AddTo(Disposer);

            _origin = new ReactiveProperty<Vector3>().AddTo(Disposer);
            _position = new ReactiveProperty<Vector3>().AddTo(Disposer);
            _velocity = new ReactiveProperty<Vector2>().AddTo(Disposer);

            TotalCollectedCurrency = Gains.ObserveAdd()
                .Select(_ => Gains.Sum())
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposer);

            Distance = _position
                .Select(position => position.x.Difference(Origin.Value.x))
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposer);

            Height = _position
                .Select(position => position.y.Difference(Origin.Value.y))
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposer);

            MaxHeightReached = Height
                .Select(height => Mathf.Max(height, MaxHeightReached.Value))
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposer);
            
            IsLanded = Observable.Merge(
                    Height.AsUnitObservable(),
                    Velocity.AsUnitObservable())
                .Select(_ =>
                {
                    var isOnGround = Height.Value.ToMeters() <= 0;
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

        public void SetRemainingShotsIfHigher(int amount)
        {
            if(amount >= ShotsRemaining.Value)
            {
                ShotsRemaining.Value = amount;
            }
        }
    }
}
