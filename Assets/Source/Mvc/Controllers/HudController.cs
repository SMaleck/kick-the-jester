using Assets.Source.App.Configuration;
using Assets.Source.Features.PlayerData;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Views;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class HudController : AbstractController
    {
        private readonly HudView _view;
        private readonly GameStateModel _gameStateModel;
        private readonly FlightStatsModel _flightStatsModel;
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly UserInputModel _userInputModel;
        private readonly CameraConfig _cameraConfig;

        public HudController(
            HudView view, 
            GameStateModel gameStateModel, 
            FlightStatsModel flightStatsModel,
            PlayerProfileModel playerProfileModel,
            UserInputModel userInputModel,
            CameraConfig cameraConfig)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _gameStateModel = gameStateModel;
            _flightStatsModel = flightStatsModel;
            _playerProfileModel = playerProfileModel;
            _userInputModel = userInputModel;
            _cameraConfig = cameraConfig;

            _view.OnPauseButtonClicked
                .Subscribe(_ => _userInputModel.OnPause.Execute())
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
                .Subscribe(value => _view.Distance = value)
                .AddTo(Disposer);

            _flightStatsModel.Height
                .Subscribe(OnHeightChanged)
                .AddTo(Disposer);

            _flightStatsModel.RelativeVelocity
                .Subscribe(value => _view.RelativeVelocity = value)
                .AddTo(Disposer);

            _flightStatsModel.Gains
                .ObserveAdd()
                .Subscribe((CollectionAddEvent<int> e) => { _view.ShowFloatingCoinAmount(e.Value); })
                .AddTo(Disposer);

            _flightStatsModel.RelativeKickForce
                .Subscribe(value => _view.RelativeKickForce = value)
                .AddTo(Disposer);

            _flightStatsModel.ShotsRemaining
                .Subscribe(_view.OnShotCountChanged)
                .AddTo(Disposer);
        }

        private void OnHeightChanged(float height)
        {
            _view.Height = height;
            _view.OutOfCameraIndicatorVisible = height >= _cameraConfig.JesterOutOfCameraY;
        }

        private void SetupProfileModel()
        {
            _playerProfileModel.BestDistance
                .Subscribe(bestDist => _view.BestDistance = bestDist)
                .AddTo(Disposer);
        }
    }
}
