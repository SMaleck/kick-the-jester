using Assets.Source.Entities.GenericComponents;
using Assets.Source.Features.PlayerData;
using Assets.Source.Mvc.Models;
using UniRx;

namespace Assets.Source.Entities.GameRound.Components
{
    public class RoundStatsRecorder : AbstractComponent<GameRoundEntity>
    {
        private readonly GameStateModel _gameStateModel;
        private readonly FlightStatsModel _flightStatsModel;
        private readonly PlayerProfileModel _playerProfileModel;

        public RoundStatsRecorder(
            GameRoundEntity owner,
            GameStateModel gameStateModel,
            FlightStatsModel flightStatsModel,
            PlayerProfileModel playerProfileModel)
            : base(owner)
        {
            _gameStateModel = gameStateModel;
            _flightStatsModel = flightStatsModel;
            _playerProfileModel = playerProfileModel;

            _gameStateModel.OnRoundEnd
                .Subscribe(_ => OnRoundEnd())
                .AddTo(Disposer);
        }

        private void OnRoundEnd()
        {
            // Record BEST DISTANCE
            var distance = _flightStatsModel.Distance.Value;
            _playerProfileModel.SetBestDistance(distance);

            // Record PLAY COUNT
            _playerProfileModel.IncrementRoundsPlayed();
        }
    }
}
