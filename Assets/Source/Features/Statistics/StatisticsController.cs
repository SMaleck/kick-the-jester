using Assets.Source.App.Configuration;
using Assets.Source.Util;

namespace Assets.Source.Features.Statistics
{
    public class StatisticsController : AbstractDisposable
    {
        private readonly StatisticsModel _statisticsModel;
        private readonly BalancingConfig _balancingConfig;

        public StatisticsController(
            StatisticsModel statisticsModel,
            BalancingConfig balancingConfig)
        {
            _statisticsModel = statisticsModel;
            _balancingConfig = balancingConfig;
        }

        public void RegisterRoundDistance(float distance)
        {
            _statisticsModel.SetBestDistanceUnits(distance);
            _statisticsModel.AddToTotalDistanceUnits(distance);
        }

        public void RegisterRoundHeight(float height)
        {
            _statisticsModel.SetBestHeightUnits(height);

            var hasReachedMoon = height.ToUnits() >= _balancingConfig.MoonHeightUnits;
            _statisticsModel.SetHasReachedMoon(hasReachedMoon);
        }

        public void RegisterRoundEnd()
        {
            _statisticsModel.IncrementTotalRoundsPlayed();
        }

        public void RegisterCurrencyCollected(int value)
        {
            _statisticsModel.AddToTotalCurrencyCollected(value);
        }
    }
}
