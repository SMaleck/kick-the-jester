using Assets.Source.App;
using Assets.Source.App.Persistence;
using Assets.Source.Behaviours.Jester;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.GameLogic.Components
{
    public class CurrencyRecorder : AbstractComponent<GameLogicContainer>
    {        
        private readonly PlayerProfileContext playerProfile;
        private readonly JesterContainer jester;
        
        private int collectedCurrency = 0;

        private float meterToCurrencyFactor = 0.5f;
        private int earnedCurrency
        {
            get { return Mathf.RoundToInt(jester.Distance.ToMeters() * meterToCurrencyFactor); }
        }


        public ReactiveCollection<int> Gains = new ReactiveCollection<int>();


        /* -------------------------------------------------------------------------- */
        public CurrencyRecorder(GameLogicContainer owner, PlayerProfileContext playerProfile, JesterContainer jester) 
            : base(owner)
        {            
            this.playerProfile = playerProfile;
            this.jester = jester;            

            owner.StateProperty
                 .Where(e => e.Equals(GameState.End))
                 .Subscribe(OnEnd)
                 .AddTo(owner);
        }


        // Commits the accumulated money pools to the PlayerProfile
        private void OnEnd(GameState state)
        {
            IDictionary<string, int> result = GetResults();

            foreach(int value in result.Values)
            {
                playerProfile.Stats.Currency += Math.Abs(value);
            }     
        }


        /* -------------------------------------------------------------------------- */
        #region INTERFACE

        // Adds money to the pickup counter
        public void AddPickup(int amount)
        {
            if (amount <= 0) { return; }
            collectedCurrency += amount;

            Gains.Add(amount);
        }


        // Returns a complete set of the earned currency for the round
        public IDictionary<string, int> GetResults()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            result.Add("from distance", earnedCurrency);
            result.Add("from pickups", collectedCurrency);

            return result;
        }

        #endregion
    }
}
