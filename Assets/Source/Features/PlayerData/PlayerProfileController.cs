using Assets.Source.Features.GameState;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Features.PlayerData
{
    public class PlayerProfileController : AbstractDisposable
    {
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly FlightStatsModel _flightStatsModel;

        public PlayerProfileController(
            PlayerProfileModel playerProfileModel,
            FlightStatsModel flightStatsModel,
            GameStateModel gameStateModel)
        {
            _playerProfileModel = playerProfileModel;
            _flightStatsModel = flightStatsModel;

            gameStateModel.OnRoundEnd
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

        public void AddCurrencyAmount(int amount)
        {
            _playerProfileModel.AddCurrencyAmount(amount);
        }
    }
}
