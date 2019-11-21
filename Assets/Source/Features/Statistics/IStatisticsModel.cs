using UniRx;

namespace Assets.Source.Features.Statistics
{
    public interface IStatisticsModel
    {
        IReadOnlyReactiveProperty<float> BestDistance { get; }
        IReadOnlyReactiveProperty<float> TotalDistance { get; }
        IReadOnlyReactiveProperty<float> BestHeight { get; }
        IReadOnlyReactiveProperty<float> TotalHeight { get; }
        IReadOnlyReactiveProperty<int> TotalCurrencyCollected { get; }
        IReadOnlyReactiveProperty<int> TotalRoundsPlayed { get; }
        IReadOnlyReactiveProperty<bool> HasReachedMoon { get; }
    }
}