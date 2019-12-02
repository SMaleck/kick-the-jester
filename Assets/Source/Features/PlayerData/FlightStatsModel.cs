using Assets.Source.Util;
using UniRx;
using UnityEngine;

namespace Assets.Source.Features.PlayerData
{
    public class FlightStatsModel : AbstractDisposable
    {
        public FloatReactiveProperty Distance;
        public ReactiveProperty<Vector2> Velocity;
      
        public FloatReactiveProperty RelativeKickForce;
        public FloatReactiveProperty RelativeVelocity;
        public IntReactiveProperty ShotsRemaining;

        public ReactiveCollection<int> Gains;
        public IntReactiveProperty Collected;

        private readonly ReactiveProperty<float> _height;
        public IReadOnlyReactiveProperty<float> Height => _height;

        private readonly ReactiveProperty<float> _maxHeightReached;
        public IReadOnlyReactiveProperty<float> MaxHeightReached => _maxHeightReached;

        public FlightStatsModel()
        {
            Distance = new FloatReactiveProperty().AddTo(Disposer);
            Velocity = new ReactiveProperty<Vector2>().AddTo(Disposer);
            RelativeKickForce = new FloatReactiveProperty().AddTo(Disposer);
            RelativeVelocity = new FloatReactiveProperty().AddTo(Disposer);
            ShotsRemaining = new IntReactiveProperty().AddTo(Disposer);
            Gains = new ReactiveCollection<int>().AddTo(Disposer);
            Collected = new IntReactiveProperty().AddTo(Disposer);

            _height = new FloatReactiveProperty().AddTo(Disposer);
            _maxHeightReached = new ReactiveProperty<float>(0).AddTo(Disposer);

            Height.Subscribe(SafeSetMaxHeightReached)
                .AddTo(Disposer);
        }

        public void SetRemainingShotsIfHigher(int amount)
        {
            if(amount >= ShotsRemaining.Value)
            {
                ShotsRemaining.Value = amount;
            }
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
