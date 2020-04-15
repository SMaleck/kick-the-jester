using Assets.Source.Util;
using UnityEngine;
using UniRx;

namespace Assets.Source.Features.PlayerData
{
    public class FlightStatsController : AbstractDisposable
    {
        private readonly FlightStatsModel _flightStatsModel;
        private readonly GameRoundStatsModel _gameRoundStatsModel;
        private readonly PlayerAttributesModel _playerAttributesModel;

        public FlightStatsController(
            FlightStatsModel flightStatsModel,
            GameRoundStatsModel gameRoundStatsModel,
            PlayerAttributesModel playerAttributesModel)
        {
            _flightStatsModel = flightStatsModel;
            _gameRoundStatsModel = gameRoundStatsModel;
            _playerAttributesModel = playerAttributesModel;

            _flightStatsModel.Velocity
                .Subscribe(OnVelocityChanged)
                .AddTo(Disposer);
        }

        private void OnVelocityChanged(Vector2 velocity)
        {
            float relativeVelocity = velocity.x.AsRelativeTo(_playerAttributesModel.MaxVelocityX.Value);
            _flightStatsModel.SetRelativeVelocity(relativeVelocity);
        }

        public void AddCurrencyPickup(int amount)
        {
            if (amount <= 0) { return; }

            _gameRoundStatsModel.AddGains(amount);
        }
    }
}
