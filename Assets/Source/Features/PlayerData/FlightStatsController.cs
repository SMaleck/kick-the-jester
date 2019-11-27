using Assets.Source.App.Configuration;
using Assets.Source.Features.GameState;
using Assets.Source.Util;
using UniRx;
using UnityEngine;

namespace Assets.Source.Features.PlayerData
{
    public class FlightStatsController : AbstractDisposable
    {
        private readonly FlightStatsModel _flightStatsModel;
        private readonly GameStateModel _gameStateModel;
        private readonly BalancingConfig _balancingConfig;
        private readonly PlayerProfileController _playerProfileController;

        public FlightStatsController(
            FlightStatsModel flightStatsModel,
            GameStateModel gameStateModel,
            BalancingConfig balancingConfig,
            PlayerProfileController playerProfileController)
        {
            _flightStatsModel = flightStatsModel;
            _gameStateModel = gameStateModel;
            _balancingConfig = balancingConfig;
            _playerProfileController = playerProfileController;

            _gameStateModel.OnRoundEnd
                .Subscribe(_ => OnRoundEnd())
                .AddTo(Disposer);
        }

        public void AddCurrencyPickup(int amount)
        {
            if (amount <= 0) { return; }

            _flightStatsModel.Gains.Add(amount);
            _flightStatsModel.Collected.Value += amount;
        }

        private void OnRoundEnd()
        {
            var distance = _flightStatsModel.Distance.Value;
            var earned = _flightStatsModel.Earned.Value;
            var totalEarned = earned + Mathf.RoundToInt(distance.ToMeters() * _balancingConfig.MeterToGoldFactor);
            _flightStatsModel.SetEarned(totalEarned);

            var total = _flightStatsModel.Collected.Value + _flightStatsModel.Earned.Value;

            _playerProfileController.AddCurrencyAmount(total);
        }
    }
}
