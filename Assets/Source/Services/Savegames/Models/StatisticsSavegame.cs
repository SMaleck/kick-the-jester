using UniRx;

namespace Assets.Source.Services.Savegames.Models
{
    public class StatisticsSavegameData : AbstractSavegameData
    {
        public float BestDistanceUnits;
        public float TotalDistanceUnits;

        public float BestHeightUnits;

        public int TotalCurrencyCollected;
        public int TotalRoundsPlayed;
        public bool HasReachedMoon;
    }

    public class StatisticsSavegame : AbstractSavegame
    {
        public readonly ReactiveProperty<float> BestDistanceUnits;
        public readonly ReactiveProperty<float> TotalDistanceUnits;

        public readonly ReactiveProperty<float> BestHeightUnits;

        public readonly ReactiveProperty<int> TotalCurrencyCollected;
        public readonly ReactiveProperty<int> TotalRoundsPlayed;
        public readonly ReactiveProperty<bool> HasReachedMoon;

        public StatisticsSavegame(StatisticsSavegameData data)
        {
            BestDistanceUnits = CreateBoundProperty(
                data.BestDistanceUnits,
                value => { data.BestDistanceUnits = value; },
                Disposer);

            TotalDistanceUnits = CreateBoundProperty(
                data.TotalDistanceUnits,
                value => { data.TotalDistanceUnits = value; },
                Disposer);

            BestHeightUnits = CreateBoundProperty(
                data.BestHeightUnits,
                value => { data.BestHeightUnits = value; },
                Disposer);

            TotalCurrencyCollected = CreateBoundProperty(
                data.TotalCurrencyCollected,
                value => { data.TotalCurrencyCollected = value; },
                Disposer);

            TotalRoundsPlayed = CreateBoundProperty(
                data.TotalRoundsPlayed,
                value => { data.TotalRoundsPlayed = value; },
                Disposer);

            HasReachedMoon = CreateBoundProperty(
                data.HasReachedMoon,
                value => { data.HasReachedMoon = value; },
                Disposer);
        }
    }
}
