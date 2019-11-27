using Assets.Source.Util;
using UniRx;
using UnityEngine;

namespace Assets.Source.Features.PlayerData
{
    public class FlightStatsModel : AbstractDisposable
    {
        public FloatReactiveProperty Distance;
        public FloatReactiveProperty Height;
        public ReactiveProperty<Vector2> Velocity;
      
        public FloatReactiveProperty RelativeKickForce;
        public FloatReactiveProperty RelativeVelocity;
        public IntReactiveProperty ShotsRemaining;

        public ReactiveCollection<int> Gains;
        public IntReactiveProperty Collected;

        private readonly ReactiveProperty<int> _earned;
        public IReadOnlyReactiveProperty<int> Earned => _earned;

        public FlightStatsModel()
        {
            Distance = new FloatReactiveProperty().AddTo(Disposer);
            Height = new FloatReactiveProperty().AddTo(Disposer);
            Velocity = new ReactiveProperty<Vector2>().AddTo(Disposer);
            RelativeKickForce = new FloatReactiveProperty().AddTo(Disposer);
            RelativeVelocity = new FloatReactiveProperty().AddTo(Disposer);
            ShotsRemaining = new IntReactiveProperty().AddTo(Disposer);
            Gains = new ReactiveCollection<int>().AddTo(Disposer);
            Collected = new IntReactiveProperty().AddTo(Disposer);

            _earned = new ReactiveProperty<int>().AddTo(Disposer);
        }

        public void SetRemainingShotsIfHigher(int amount)
        {
            if(amount >= ShotsRemaining.Value)
            {
                ShotsRemaining.Value = amount;
            }
        }

        public void SetEarned(int value)
        {
            _earned.Value = value;
        }
    }
}
