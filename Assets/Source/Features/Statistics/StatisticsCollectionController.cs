﻿using Assets.Source.Features.GameState;
using Assets.Source.Features.PlayerData;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Features.Statistics
{
    public class StatisticsCollectionController : AbstractDisposable
    {
        private readonly StatisticsController _statisticsController;
        private readonly FlightStatsModel _flightStatsModel;

        public StatisticsCollectionController(
            StatisticsController statisticsController,
            StatisticsModel statisticsModel,
            GameStateModel gameStateModel,
            FlightStatsModel flightStatsModel,
            PlayerProfileModel playerProfileModel)
        {
            _statisticsController = statisticsController;
            _flightStatsModel = flightStatsModel;

            gameStateModel.OnRoundEnd
                .Subscribe(_ => OnRoundEnd())
                .AddTo(Disposer);

            playerProfileModel.CurrencyAmount
                .Pairwise()
                .Where(pair => pair.Current > pair.Previous)
                .Select(pair => pair.Current - pair.Previous)
                .Subscribe(statisticsController.RegisterCurrencyCollected)
                .AddTo(Disposer);

            _flightStatsModel.Height
                .Where(height => height > statisticsModel.BestHeight.Value)
                .Subscribe(statisticsController.RegisterRoundHeight)
                .AddTo(Disposer);
        }

        private void OnRoundEnd()
        {
            var distance = _flightStatsModel.Distance.Value;
            _statisticsController.RegisterRoundDistance(distance);

            _statisticsController.RegisterRoundEnd();
        }
    }
}