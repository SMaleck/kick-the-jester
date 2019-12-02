﻿using Assets.Source.Features.GameState;
using Assets.Source.Features.PlayerData;
using Assets.Source.Features.Statistics;
using Assets.Source.Mvc.Mediation;
using Assets.Source.Mvc.Models.Enum;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Util;
using System.Collections.Generic;
using System.Linq;
using Assets.Source.App.Configuration;
using UniRx;
using UnityEngine;

namespace Assets.Source.Mvc.Controllers
{
    public class RoundEndController : AbstractDisposable
    {
        private readonly RoundEndView _view;
        private readonly GameStateModel _gameStateModel;
        private readonly FlightStatsModel _flightStatsModel;
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly IStatisticsModel _statisticsModel;
        private readonly SceneTransitionService _sceneTransitionService;
        private readonly PlayerProfileController _playerProfileController;
        private readonly IClosableViewMediator _closableViewMediator;
        private readonly BalancingConfig _balancingConfig;

        private readonly int _currencyAmountAtStart;
        private readonly float _bestDistanceAtStart;

        public RoundEndController(
            RoundEndView view,
            GameStateModel gameStateModel,
            FlightStatsModel flightStatsModel,
            PlayerProfileModel playerProfileModel,
            IStatisticsModel statisticsModel,
            SceneTransitionService sceneTransitionService,
            PlayerProfileController playerProfileController,
            IClosableViewMediator closableViewMediator,
            BalancingConfig balancingConfig)
        {
            _view = view;

            _gameStateModel = gameStateModel;
            _flightStatsModel = flightStatsModel;
            _playerProfileModel = playerProfileModel;
            _statisticsModel = statisticsModel;
            _sceneTransitionService = sceneTransitionService;
            _playerProfileController = playerProfileController;
            _closableViewMediator = closableViewMediator;
            _balancingConfig = balancingConfig;

            _currencyAmountAtStart = _playerProfileModel.CurrencyAmount.Value;
            _bestDistanceAtStart = _statisticsModel.BestDistance.Value;

            _view.OnRetryClicked
                .Subscribe(_ => OnRetryClicked())
                .AddTo(Disposer);

            _view.OnUpgradesClicked
                .Subscribe(_ => _closableViewMediator.Open(ClosableViewType.Upgrades))
                .AddTo(Disposer);

            _closableViewMediator.OnViewOpened
                .Where(viewType => viewType == ClosableViewType.RoundEnd)
                .DelayFrame(1)
                .Subscribe(_ => OnOpenCompleted())
                .AddTo(Disposer);

            SetupModelSubscriptions();
        }

        private void SetupModelSubscriptions()
        {
            _gameStateModel.OnRoundEnd
                .Subscribe(_ => _closableViewMediator.Open(ClosableViewType.RoundEnd))
                .AddTo(Disposer);

            _flightStatsModel.Distance
                .Subscribe(_view.SetDistance)
                .AddTo(Disposer);

            _statisticsModel.BestDistance
                .Subscribe(bestDist =>
                {
                    _view.SetIsNewBestDistance(_bestDistanceAtStart < bestDist);
                    _view.SetBestDistance(bestDist);
                })
                .AddTo(Disposer);
        }

        private void OnRetryClicked()
        {
            _sceneTransitionService.ToGame();
        }

        private void OnOpenCompleted()
        {
            var currencyGainResults = CalculateCurrencyGainResult();
            ProcessRoundResults(currencyGainResults);

            _view.ShowCurrencyResults(currencyGainResults, _currencyAmountAtStart);
        }

        private void ProcessRoundResults(IDictionary<CurrencyGainType, int> currencyGainResults)
        {
            var totalGained = currencyGainResults.Values.Sum();
            _playerProfileController.AddCurrencyAmount(totalGained);
        }

        private IDictionary<CurrencyGainType, int> CalculateCurrencyGainResult()
        {
            Dictionary<CurrencyGainType, int> result = new Dictionary<CurrencyGainType, int>();

            var distanceUnits = _flightStatsModel.Distance.Value;

            result.Add(CurrencyGainType.Distance, GetGainFromDistance(distanceUnits));
            result.Add(CurrencyGainType.Pickup, _flightStatsModel.Collected.Value);

            return result;
        }

        private int GetGainFromDistance(float distanceUnits)
        {
            return Mathf.RoundToInt(distanceUnits.ToMeters() * _balancingConfig.MeterToGoldFactor);
        }
    }
}
