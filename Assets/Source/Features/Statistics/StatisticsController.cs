﻿using Assets.Source.Util;

namespace Assets.Source.Features.Statistics
{
    public class StatisticsController : AbstractDisposable
    {
        private const float HasReachedMoonHeightUnits = 230;

        private readonly StatisticsModel _statisticsModel;

        public StatisticsController(StatisticsModel statisticsModel)
        {
            _statisticsModel = statisticsModel;
        }

        public void RegisterRoundDistance(float distance)
        {
            _statisticsModel.SetBestDistance(distance);
            _statisticsModel.AddToTotalDistance(distance);
        }

        public void RegisterRoundHeight(float height)
        {
            _statisticsModel.SetBestHeight(height);

            var hasReachedMoon = height.ToUnits() >= HasReachedMoonHeightUnits;
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