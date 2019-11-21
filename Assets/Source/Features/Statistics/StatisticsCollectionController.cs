using Assets.Source.Features.GameState;
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
            FlightStatsModel flightStatsModel,
            GameStateModel gameStateModel)
        {
            _statisticsController = statisticsController;
            _flightStatsModel = flightStatsModel;

            gameStateModel.OnRoundEnd
                .Subscribe(_ => OnRoundEnd())
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
