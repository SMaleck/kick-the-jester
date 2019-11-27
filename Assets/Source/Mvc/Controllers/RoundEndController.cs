using Assets.Source.Features.GameState;
using Assets.Source.Features.PlayerData;
using Assets.Source.Features.Statistics;
using Assets.Source.Mvc.Mediation;
using Assets.Source.Mvc.Models.Enum;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Util;
using System.Collections.Generic;
using UniRx;

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
        private readonly IClosableViewMediator _closableViewMediator;

        private readonly int _currencyAmountAtStart;
        private readonly float _bestDistanceAtStart;

        public RoundEndController(
            RoundEndView view,
            GameStateModel gameStateModel,
            FlightStatsModel flightStatsModel,
            PlayerProfileModel playerProfileModel,
            IStatisticsModel statisticsModel,
            SceneTransitionService sceneTransitionService,
            IClosableViewMediator closableViewMediator)
        {
            _view = view;

            _gameStateModel = gameStateModel;
            _flightStatsModel = flightStatsModel;
            _playerProfileModel = playerProfileModel;
            _statisticsModel = statisticsModel;
            _sceneTransitionService = sceneTransitionService;
            _closableViewMediator = closableViewMediator;

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

        private void OnOpenCompleted()
        {
            var results = GetResultsAsDictionary();

            _view.ShowCurrencyResults(results, _currencyAmountAtStart);
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
    }
}
