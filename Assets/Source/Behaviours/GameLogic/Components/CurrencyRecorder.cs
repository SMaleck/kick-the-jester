using Assets.Source.App;
using Assets.Source.App.Storage;
using Assets.Source.Behaviours.Jester;
using Assets.Source.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.GameLogic.Components
{
    public class CurrencyRecorder : AbstractComponent<GameLogicContainer>
    {        
        private readonly PlayerProfileService playerProfileService;
        private readonly JesterContainer jester;

        private int collectedCurrency = 0;

        private float meterToCurrencyFactor = 0.5f;
        private int earnedCurrency
        {
            get { return Mathf.RoundToInt(jester.Distance.ToMeters() * meterToCurrencyFactor); }
        }


        /* -------------------------------------------------------------------------- */
        public CurrencyRecorder(GameLogicContainer owner, PlayerProfileService playerProfileService, JesterContainer jester) 
            : base(owner)
        {            
            this.playerProfileService = playerProfileService;
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
                playerProfileService.Currency += Math.Abs(value);
            }     
        }


        /* -------------------------------------------------------------------------- */
        #region INTERFACE

        // Adds money to the pickup counter
        public void AddPickup(int amount)
        {
            if (amount <= 0) { return; }
            collectedCurrency += amount;
        }


        // Returns a complete set of the earned currency for the round
        public IDictionary<string, int> GetResults()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            result.Add("from distance", collectedCurrency);
            result.Add("from pickups", earnedCurrency);

            return result;
        }

        #endregion
    }
}
