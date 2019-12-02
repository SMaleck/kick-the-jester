using System.Linq;
using Assets.Source.Util;
using UniRx;
using UnityEngine;

namespace Assets.Source.Features.PlayerData
{
    public class FlightStatsModel : AbstractDisposable
    {
        public ReactiveProperty<Vector2> Velocity;
      
        public FloatReactiveProperty RelativeKickForce;
        public FloatReactiveProperty RelativeVelocity;
        public IntReactiveProperty ShotsRemaining;

        public ReactiveCollection<int> Gains;

        private readonly ReactiveProperty<float> _distance;
        public IReadOnlyReactiveProperty<float> Distance => _distance;

        private readonly ReactiveProperty<float> _height;
        public IReadOnlyReactiveProperty<float> Height => _height;

        private readonly ReactiveProperty<float> _maxHeightReached;
        public IReadOnlyReactiveProperty<float> MaxHeightReached => _maxHeightReached;

        public IReadOnlyReactiveProperty<int> TotalCollectedCurrency;

        public FlightStatsModel()
        {
            Velocity = new ReactiveProperty<Vector2>().AddTo(Disposer);
            RelativeKickForce = new FloatReactiveProperty().AddTo(Disposer);
            RelativeVelocity = new FloatReactiveProperty().AddTo(Disposer);
            ShotsRemaining = new IntReactiveProperty().AddTo(Disposer);
            Gains = new ReactiveCollection<int>().AddTo(Disposer);

            _distance = new FloatReactiveProperty().AddTo(Disposer);
            _height = new FloatReactiveProperty().AddTo(Disposer);
            _maxHeightReached = new ReactiveProperty<float>(0).AddTo(Disposer);

            Height.Subscribe(SafeSetMaxHeightReached)
                .AddTo(Disposer);

            TotalCollectedCurrency = Gains.ObserveAdd()
                .Select(_ => Gains.Sum())
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposer);
        }

        public void SetRemainingShotsIfHigher(int amount)
        {
            if(amount >= ShotsRemaining.Value)
            {
                ShotsRemaining.Value = amount;
            }
        }

        public void SetDistance(float value)
        {
            _distance.Value = value;
        }

        public void SetHeight(float value)
        {
            _height.Value = value;
        }

        private void SafeSetMaxHeightReached(float value)
        {
            _maxHeightReached.Value = Mathf.Max(value, _maxHeightReached.Value);
        }
    }
}
