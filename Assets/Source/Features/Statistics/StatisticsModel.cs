using Assets.Source.Services.Savegames;
using Assets.Source.Services.Savegames.Models;
using Assets.Source.Util;
using System;
using UniRx;

namespace Assets.Source.Features.Statistics
{
    public class StatisticsModel : AbstractDisposable, IStatisticsModel
    {
        private readonly StatisticsSavegame _statisticsSavegame;

        public IReadOnlyReactiveProperty<float> BestHeightUnits => _statisticsSavegame.BestHeightUnits;
        public IReadOnlyReactiveProperty<float> BestDistanceUnits => _statisticsSavegame.BestDistanceUnits;
        public IReadOnlyReactiveProperty<float> TotalDistanceUnits => _statisticsSavegame.TotalDistanceUnits;

        public IReadOnlyReactiveProperty<int> TotalCurrencyCollected => _statisticsSavegame.TotalCurrencyCollected;
        public IReadOnlyReactiveProperty<int> TotalRoundsPlayed => _statisticsSavegame.TotalRoundsPlayed;
        public IReadOnlyReactiveProperty<bool> HasReachedMoon => _statisticsSavegame.HasReachedMoon;

        public StatisticsModel(ISavegameService savegameService)
        {
            _statisticsSavegame = savegameService.Savegame.StatisticsSavegame;
        }

        public void SetBestDistanceUnits(float value)
        {
            var currentBestDistance = _statisticsSavegame.BestDistanceUnits.Value;
            _statisticsSavegame.BestDistanceUnits.Value = Math.Max(currentBestDistance, value);
        }

        public void AddToTotalDistanceUnits(float value)
        {
            _statisticsSavegame.TotalDistanceUnits.Value += value;
        }

        public void SetBestHeightUnits(float value)
        {
            var current = _statisticsSavegame.BestHeightUnits.Value;
            _statisticsSavegame.BestHeightUnits.Value = Math.Max(current, value);
        }

        public void AddToTotalCurrencyCollected(int value)
        {
            _statisticsSavegame.TotalCurrencyCollected.Value += value;
        }

        public void IncrementTotalRoundsPlayed()
        {
            _statisticsSavegame.TotalRoundsPlayed.Value += 1;
        }

        public void SetHasReachedMoon(bool value)
        {
            _statisticsSavegame.HasReachedMoon.Value = value;
        }
    }
}
