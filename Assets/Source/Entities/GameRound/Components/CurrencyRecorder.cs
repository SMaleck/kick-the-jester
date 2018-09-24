using Assets.Source.Entities.GenericComponents;
using Assets.Source.Mvc.Models;
using Assets.Source.Util;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.GameRound.Components
{
    public class CurrencyRecorder : AbstractComponent<GameRoundEntity>
    {
        private const float MeterToGoldFactor = 0.5f;        
        private readonly FlightStatsModel _flightStatsModel;


        public CurrencyRecorder(GameRoundEntity owner, FlightStatsModel flightStatsModel) : base(owner)
        {            
            _flightStatsModel = flightStatsModel;

            flightStatsModel.Distance
                .Subscribe(OnDistanceChanged)
                .AddTo(owner);
        }

        private void OnDistanceChanged(float distance)
        {
            _flightStatsModel.Earned.Value += Mathf.RoundToInt(distance.ToMeters() * MeterToGoldFactor);
        }

        // Adds money to the pickup counter
        public void AddPickup(int amount)
        {
            if (amount <= 0) { return; }

            _flightStatsModel.Gains.Add(amount);
            _flightStatsModel.Collected.Value += amount;
        }


        // Returns a complete set of the earned currency for the round
        public IDictionary<string, int> GetResults()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            result.Add("from distance", _flightStatsModel.Earned.Value);
            result.Add("from pickups", _flightStatsModel.Collected.Value);

            return result;
        }
    }
}
