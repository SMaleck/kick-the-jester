using Assets.Source.Entities.GenericComponents;
using Assets.Source.Mvc.Models;
using Assets.Source.Util;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.GameRound.Components
{
    public class CurrencyGainEvent
    {
        public int Amount;
    }

    public class CurrencyRecorder : AbstractComponent<GameRoundEntity>
    {
        // ToDo [CONFIG] Move to config SO
        private const float MeterToGoldFactor = 0.5f;

        private readonly GameStateModel _gameStateModel;
        private readonly FlightStatsModel _flightStatsModel;
        private readonly ProfileModel _profileModel;


        public CurrencyRecorder(GameRoundEntity owner, GameStateModel gameStateModel, FlightStatsModel flightStatsModel, ProfileModel profileModel) 
            : base(owner)
        {
            _gameStateModel = gameStateModel;
            _flightStatsModel = flightStatsModel;
            _profileModel = profileModel;
            
            _gameStateModel.OnRoundEnd
                .Subscribe(_ => OnRoundEnd())
                .AddTo(owner);

            MessageBroker.Default.Receive<CurrencyGainEvent>()
                .Subscribe(gainEvent => AddPickup(gainEvent.Amount))
                .AddTo(owner);
        }

        private void OnRoundEnd()
        {
            var distance = _flightStatsModel.Distance.Value;
            _flightStatsModel.Earned.Value += Mathf.RoundToInt(distance.ToMeters() * MeterToGoldFactor);

            var total = _flightStatsModel.Collected.Value + _flightStatsModel.Earned.Value;
            _profileModel.Currency.Value += total;
        }

        // Adds money to the pickup counter
        public void AddPickup(int amount)
        {
            if (amount <= 0) { return; }

            _flightStatsModel.Gains.Add(amount);
            _flightStatsModel.Collected.Value += amount;
        }        
    }
}
