using Assets.Source.Features.PlayerData;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using System.Collections.Generic;
using Assets.Source.Mvc.Models.Enum;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class RoundEndController : ClosableController
    {
        private readonly RoundEndView _view;
        private readonly GameStateModel _gameStateModel;
        private readonly FlightStatsModel _flightStatsModel;
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly SceneTransitionService _sceneTransitionService;

        private readonly int currencyAmountAtStart;
        private readonly float bestDistanceAtStart;

        public RoundEndController(
            RoundEndView view,
            OpenPanelModel openPanelModel,
            GameStateModel gameStateModel,
            FlightStatsModel flightStatsModel,
            PlayerProfileModel playerProfileModel,
            SceneTransitionService sceneTransitionService)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _gameStateModel = gameStateModel;
            _flightStatsModel = flightStatsModel;
            _playerProfileModel = playerProfileModel;
            _sceneTransitionService = sceneTransitionService;

            currencyAmountAtStart = _playerProfileModel.CurrencyAmount.Value;
            bestDistanceAtStart = _playerProfileModel.BestDistance.Value;

            _view.OnRetryClicked
                .Subscribe(_ => OnRetryClicked())
                .AddTo(Disposer);

            _view.OnUpgradesClicked
                .Subscribe(_ => openPanelModel.OpenUpgrades())
                .AddTo(Disposer);

            _view.OnOpenCompleted
                .Subscribe(_ => OnOpenCompleted())
                .AddTo(Disposer);

            SetupModelSubscriptions();
        }

        private void OnOpenCompleted()
        {
            var results = GetResultsAsDictionary();

            _view.ShowCurrencyResults(results, currencyAmountAtStart);
        }

        private IDictionary<CurrencyGainType, int> GetResultsAsDictionary()
        {
            Dictionary<CurrencyGainType, int> result = new Dictionary<CurrencyGainType, int>();

            result.Add(CurrencyGainType.Distance, _flightStatsModel.Earned.Value);
            result.Add(CurrencyGainType.Pickup, _flightStatsModel.Collected.Value);

            return result;
        }

        private void SetupModelSubscriptions()
        {
            _gameStateModel.OnRoundEnd
                .Subscribe(_ => Open())
                .AddTo(Disposer);

            _flightStatsModel.Distance
                .Subscribe(dist => _view.Distance = dist)
                .AddTo(Disposer);

            _playerProfileModel.BestDistance
                .Subscribe(bestDist =>
                {
                    _view.IsNewBestDistance = bestDistanceAtStart < bestDist;
                    _view.BestDistance = bestDist;
                })
                .AddTo(Disposer);
        }

        private void OnRetryClicked()
        {
            _sceneTransitionService.ToGame();
        }
    }
}
