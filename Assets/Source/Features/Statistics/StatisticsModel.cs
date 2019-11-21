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

        public IReadOnlyReactiveProperty<float> BestDistance => _statisticsSavegame.BestDistance;
        public IReadOnlyReactiveProperty<float> TotalDistance => _statisticsSavegame.TotalDistance;

        public IReadOnlyReactiveProperty<float> BestHeight => _statisticsSavegame.BestHeight;
        public IReadOnlyReactiveProperty<float> TotalHeight => _statisticsSavegame.TotalHeight;

        public IReadOnlyReactiveProperty<int> TotalCurrencyCollected => _statisticsSavegame.TotalCurrencyCollected;
        public IReadOnlyReactiveProperty<int> TotalRoundsPlayed => _statisticsSavegame.TotalRoundsPlayed;
        public IReadOnlyReactiveProperty<bool> HasReachedMoon => _statisticsSavegame.HasReachedMoon;

        public StatisticsModel(ISavegameService savegameService)
        {
            _statisticsSavegame = savegameService.Savegame.StatisticsSavegame;
        }

        public void SetBestDistance(float value)
        {
            var currentBestDistance = _statisticsSavegame.BestDistance.Value;
            _statisticsSavegame.BestDistance.Value = Math.Max(currentBestDistance, value);
        }

        public void AddToTotalDistance(float value)
        {
            _statisticsSavegame.TotalDistance.Value += value;
        }

        public void SetBestHeight(float value)
        {
            var current = _statisticsSavegame.BestHeight.Value;
            _statisticsSavegame.BestHeight.Value = Math.Max(current, value);
        }

        public void AddToTotalHeight(float value)
        {
            _statisticsSavegame.TotalHeight.Value += value;
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
