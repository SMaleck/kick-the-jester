using System;
using Assets.Source.Features.GameState;
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

            EnumHelper<CurrencyGainType>.Iterator
                .ToList()
                .ForEach(currencyGainType =>
                {
                    var gainAmount = GetGainFrom(currencyGainType);
                    if (gainAmount > 0)
                    {
                        result.Add(currencyGainType, gainAmount);
                    }
                });

            return result;
        }

        private int GetGainFrom(CurrencyGainType currencyGainType)
        {
            switch (currencyGainType)
            {
                case CurrencyGainType.Distance:
                    return GetGainFromDistance(_flightStatsModel);

                case CurrencyGainType.Pickup:
                    return GetGainFromPickup(_flightStatsModel);

                case CurrencyGainType.ShortDistanceBonus:
                    return GetGainFromShortDistanceBonus(_flightStatsModel);

                case CurrencyGainType.MaxHeightBonus:
                    return GetGainFromMaxHeightBonus(_flightStatsModel);

                default:
                    throw new ArgumentOutOfRangeException(nameof(currencyGainType), currencyGainType, null);
            }
        }

        private int GetGainFromDistance(FlightStatsModel flightStatsModel)
        {
            var distanceUnits = flightStatsModel.Distance.Value;
            return Mathf.RoundToInt(distanceUnits.ToMeters() * _balancingConfig.MeterToGoldFactor);
        }

        private int GetGainFromPickup(FlightStatsModel flightStatsModel)
        {
            return flightStatsModel.Gains.Sum();
        }

        private int GetGainFromShortDistanceBonus(FlightStatsModel flightStatsModel)
        {
            return _flightStatsModel.Distance.Value <= _balancingConfig.ShortDistanceUnits
                ? _balancingConfig.ShortDistanceGoldBonus
                : 0;
        }

        private int GetGainFromMaxHeightBonus(FlightStatsModel flightStatsModel)
        {
            return _flightStatsModel.MaxHeightReached.Value >= _balancingConfig.MoonHeightUnits
                ? _balancingConfig.MaxHeightGoldBonus
                : 0;
        }
    }
}
