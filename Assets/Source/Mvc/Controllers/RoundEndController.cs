using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    // ToDo delay opening
    public class RoundEndController : ClosableController
    {
        private readonly RoundEndView _view;
        private readonly GameStateModel _gameStateModel;
        private readonly FlightStatsModel _flightStatsModel;
        private readonly SceneTransitionService _sceneTransitionService;

        public RoundEndController(RoundEndView view, GameStateModel gameStateModel, FlightStatsModel flightStatsModel, SceneTransitionService sceneTransitionService)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _gameStateModel = gameStateModel;
            _flightStatsModel = flightStatsModel;
            _sceneTransitionService = sceneTransitionService;

            _view.OnRetryClicked
                .Subscribe(_ => OnRetryClicked())
                .AddTo(Disposer);

            _view.OnShopClicked
                .Subscribe(_ => OnShopClicked())
                .AddTo(Disposer);

            _gameStateModel.OnRoundEnd
                .Subscribe(_ => Open())
                .AddTo(Disposer);

            SetupFlightStatsSubscriptions();
        }

        private void SetupFlightStatsSubscriptions()
        {
            _flightStatsModel.Distance
                .Subscribe(dist => _view.Distance = dist)
                .AddTo(Disposer);
        }

        private void OnRetryClicked()
        {
            _sceneTransitionService.ToGame();
        }


        // ToDo Open Shop
        private void OnShopClicked() { }
    }
}
