using Assets.Source.Util;
using System.Linq;
using UniRx;

namespace Assets.Source.Features.PlayerData
{
    public class GameRoundStatsModel : AbstractDisposable
    {
        private readonly ReactiveProperty<float> _relativeKickForce;
        public IReadOnlyReactiveProperty<float> RelativeKickForce => _relativeKickForce;

        private readonly ReactiveProperty<int> _shotsRemaining;
        public IReadOnlyReactiveProperty<int> ShotsRemaining => _shotsRemaining;

        private readonly ReactiveCollection<int> _gains;
        public IReadOnlyReactiveCollection<int> Gains => _gains;

        public IReadOnlyReactiveProperty<int> TotalCollectedCurrency;

        public GameRoundStatsModel()
        {
            _relativeKickForce = new ReactiveProperty<float>().AddTo(Disposer);
            _shotsRemaining = new ReactiveProperty<int>().AddTo(Disposer);
            _gains = new ReactiveCollection<int>().AddTo(Disposer);

            TotalCollectedCurrency = Gains.ObserveAdd()
                .Select(_ => Gains.Sum())
                .ToReadOnlyReactiveProperty()
                .AddTo(Disposer);
        }

        public void SetRelativeKickForce(float value)
        {
            _relativeKickForce.Value = value;
        }

        public void SetRemainingShotsIfHigher(int amount)
        {
            if (amount >= ShotsRemaining.Value)
            {
                _shotsRemaining.Value = amount;
            }
        }

        public void DecrementShotsRemaining()
        {
            _shotsRemaining.Value--;
        }

        public void AddGains(int value)
        {
            _gains.Add(value);
        }
    }
}
