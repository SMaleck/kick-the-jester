﻿using Assets.Source.App.Configuration;
using Assets.Source.Features.GameState;
using Assets.Source.Features.PlayerData;
using Assets.Source.Features.Statistics;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Views;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class HudController : AbstractDisposable
    {
        private readonly HudView _view;
        private readonly GameStateModel _gameStateModel;
        private readonly FlightStatsModel _flightStatsModel;
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly IStatisticsModel _statisticsModel;
        private readonly UserInputModel _userInputModel;
        private readonly CameraConfig _cameraConfig;

        public HudController(
            HudView view,
            GameStateModel gameStateModel,
            FlightStatsModel flightStatsModel,
            PlayerProfileModel playerProfileModel,
            IStatisticsModel statisticsModel,
            UserInputModel userInputModel,
            CameraConfig cameraConfig)
        {
            _view = view;

            _gameStateModel = gameStateModel;
            _flightStatsModel = flightStatsModel;
            _playerProfileModel = playerProfileModel;
            _statisticsModel = statisticsModel;
            _userInputModel = userInputModel;
            _cameraConfig = cameraConfig;

            _view.OnPauseButtonClicked
                .Subscribe(_ => _userInputModel.PublishOnPause())
                .AddTo(Disposer);

            SetupGameStateModel();
            SetupFlightStatsModel();
            SetupProfileModel();
        }

        private void SetupGameStateModel()
        {
            _gameStateModel.OnRoundStart
                .Subscribe(_ => _view.StartRound())
                .AddTo(Disposer);
        }

        private void SetupFlightStatsModel()
        {
            _flightStatsModel.Distance
                .Subscribe(_view.SetDistance)
                .AddTo(Disposer);

            _view.SetCollectedCurrencyAmount(0);
            _flightStatsModel.TotalCollectedCurrency
                .Subscribe(_view.SetCollectedCurrencyAmount)
                .AddTo(Disposer);

            _flightStatsModel.Height
                .Subscribe(OnHeightChanged)
                .AddTo(Disposer);

            _flightStatsModel.RelativeVelocity
                .Subscribe(_view.SetRelativeVelocity)
                .AddTo(Disposer);

            _flightStatsModel.Gains
                .ObserveAdd()
                .Subscribe(addEvent => { _view.ShowFloatingCoinAmount(addEvent.Value); })
                .AddTo(Disposer);

            _flightStatsModel.RelativeKickForce
                .Subscribe(_view.SetRelativeKickForce)
                .AddTo(Disposer);

            _flightStatsModel.ShotsRemaining
                .Subscribe(_view.SetProjectileAmount)
                .AddTo(Disposer);
        }

        private void OnHeightChanged(float height)
        {
            _view.SetOutOfCameraIndicatorVisible(height >= _cameraConfig.JesterOutOfCameraY);
        }

        private void SetupProfileModel()
        {
            _statisticsModel.BestDistanceUnits
                .Subscribe(_view.SetBestDistance)
                .AddTo(Disposer);

            _playerProfileModel.CurrencyAmount
                .Subscribe(_view.SetCurrencyAmount)
                .AddTo(Disposer);
        }
    }
}
