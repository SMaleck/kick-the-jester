using Assets.Source.App;
using Assets.Source.App.Storage;
using Assets.Source.Models;
using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.GameLogic.Components
{
    public class CurrencyRecorder : AbstractComponent<GameLogicContainer>
    {        
        private readonly PlayerProfileService playerProfileService;

        public CurrencyRecorder(GameLogicContainer owner, PlayerProfileService playerProfileService) 
            : base(owner)
        {            
            this.playerProfileService = playerProfileService;

            HasCommitted = false;

            owner.StateProperty
                 .Where(e => e.Equals(GameState.End))
                 .Subscribe(OnEnd)
                 .AddTo(owner);
        }

        public IntReactiveProperty CurrencyCollectedProperty = new IntReactiveProperty(0);
        public int CurrencyCollected
        {
            get { return CurrencyCollectedProperty.Value; }
            set { CurrencyCollectedProperty.Value = value; }
        }

        public bool HasCommitted { get; private set; }
        private event NotifyEventHandler _OnCommit = delegate { };
        public void OnCommit(NotifyEventHandler handler)
        {
            _OnCommit += handler;
            if (HasCommitted) handler();
        }


        // Commit Money to profile, if the game ended, or we are switching
        private void OnEnd(GameState state)
        {
            TryCommitPools();
        }

        public void AddPickup(int amount)
        {
            if (amount <= 0) { return; }
            CurrencyCollected += amount;
        }


        /// <summary>
        /// Commits the accumulated money pools to the PlayerProfile
        /// </summary>
        /// <returns></returns>
        private bool TryCommitPools()
        {
            playerProfileService.Currency += Math.Abs(CurrencyCollected) + Math.Abs(CalculateCurrencyEarnedInFlight());
            _OnCommit();
            return true;
        }

        public int CalculateCurrencyEarnedInFlight()
        {
            // 10% of the distance achieved
            return Mathf.RoundToInt(App.Cache.Jester.Distance.ToMeters() * 0.1f);
        }
    }
}
