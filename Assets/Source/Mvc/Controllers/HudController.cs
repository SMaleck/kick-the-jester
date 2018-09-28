using Assets.Source.Entities.GameRound.Components;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class HudController : AbstractController
    {
        private readonly HudView _view;
        private readonly FlightStatsModel _flightStatsModel;
        private readonly GameStateModel _gameStateModel;
        private readonly CurrencyRecorder _currencyRecorder;
        private readonly UserControlService _userControlService;

        public HudController(HudView view, FlightStatsModel flightStatsModel, GameStateModel gameStateModel, UserControlService userControlService)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _flightStatsModel = flightStatsModel;
            _gameStateModel = gameStateModel;            
            _userControlService = userControlService;

            _view.OnPauseButtonClicked
                .Subscribe(_ => _userControlService.OnPause.Execute())
                .AddTo(Disposer);

            SetupGameStateModel();
            SetupFlightStatsModel();
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
                .Subscribe(value => _view.Height = value)
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
    }
}
