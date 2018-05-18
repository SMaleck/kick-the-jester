using Assets.Source.App;
using Assets.Source.Models;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    public class CurrencyRecorder : MonoBehaviour
    {
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

        private void Awake()
        {
            HasCommitted = false;
            App.Cache.GameStateMachine.StateProperty
                                      .Where(e => e.Equals(GameState.End))
                                      .Subscribe(OnEnd)
                                      .AddTo(this);
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
            Kernel.PlayerProfileService.Currency += Math.Abs(CurrencyCollected) + Math.Abs(CalculateCurrencyEarnedInFlight());
            _OnCommit();
            return true;
        }

        public int CalculateCurrencyEarnedInFlight()
        {
            // 10% of the distance achieved
            return Mathf.RoundToInt(App.Cache.jester.Distance.ToMeters() * 0.1f);
        }
    }
}
