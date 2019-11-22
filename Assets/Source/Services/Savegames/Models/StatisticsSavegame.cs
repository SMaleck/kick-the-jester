using UniRx;

namespace Assets.Source.Services.Savegames.Models
{
    public class StatisticsSavegameData : AbstractSavegameData
    {
        public float BestDistance;
        public float TotalDistance;

        public float BestHeight;

        public int TotalCurrencyCollected;
        public int TotalRoundsPlayed;
        public bool HasReachedMoon;
    }

    public class StatisticsSavegame : AbstractSavegame
    {
        public readonly ReactiveProperty<float> BestDistance;
        public readonly ReactiveProperty<float> TotalDistance;

        public readonly ReactiveProperty<float> BestHeight;

        public readonly ReactiveProperty<int> TotalCurrencyCollected;
        public readonly ReactiveProperty<int> TotalRoundsPlayed;
        public readonly ReactiveProperty<bool> HasReachedMoon;

        public StatisticsSavegame(StatisticsSavegameData data)
        {
            BestDistance = CreateBoundProperty(
                data.BestDistance,
                value => { data.BestDistance = value; },
                Disposer);

            TotalDistance = CreateBoundProperty(
                data.TotalDistance,
                value => { data.TotalDistance = value; },
                Disposer);

            BestHeight = CreateBoundProperty(
                data.BestHeight,
                value => { data.BestHeight = value; },
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
