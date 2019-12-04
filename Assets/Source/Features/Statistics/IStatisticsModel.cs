using UniRx;

namespace Assets.Source.Features.Statistics
{
    public interface IStatisticsModel
    {
        IReadOnlyReactiveProperty<float> BestDistanceUnits { get; }
        IReadOnlyReactiveProperty<float> TotalDistanceUnits { get; }
        IReadOnlyReactiveProperty<float> BestHeightUnits { get; }
        IReadOnlyReactiveProperty<int> TotalCurrencyCollected { get; }
        IReadOnlyReactiveProperty<int> TotalRoundsPlayed { get; }
        IReadOnlyReactiveProperty<bool> HasReachedMoon { get; }
    }
}