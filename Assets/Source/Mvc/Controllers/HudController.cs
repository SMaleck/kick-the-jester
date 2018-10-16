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
        private readonly ProfileModel _profileModel;
        private readonly UserInputModel _userInputModel;


        public HudController(HudView view, GameStateModel gameStateModel, FlightStatsModel flightStatsModel, ProfileModel profileModel, UserInputModel userInputModel)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _gameStateModel = gameStateModel;
            _flightStatsModel = flightStatsModel;
            _profileModel = profileModel;
            _userInputModel = userInputModel;

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

        private void SetupProfileModel()
        {
            _profileModel.BestDistance
                .Subscribe(bestDist => _view.BestDistance = bestDist)
                .AddTo(Disposer);
        }
    }
}
