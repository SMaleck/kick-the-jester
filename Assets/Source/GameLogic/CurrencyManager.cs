using Assets.Source.Models;
using System;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    public class CurrencyManager : MonoBehaviour
    {
        #region PROPERTIES

        private bool CanDoTransactions
        {
            get { return App.Cache.playerProfile.IsLoaded; }
        }
            

        // COLLECTED during Flight
        private event IntValueEventHandler _OnCollectedChanged = delegate { };
        public void OnCollectedChanged(IntValueEventHandler handler)
        {
            _OnCollectedChanged += handler;
            handler(_collected);
        }

        private int _collected = 0;
        public int Collected
        {
            get { return _collected; }
            set
            {
                _collected = value;
                _OnCollectedChanged(_collected);
            }
        }

        // EARNED after the round ended
        private event IntValueEventHandler _OnEarnedChanged = delegate { };
        public void OnEarnedChanged(IntValueEventHandler handler)
        {
            _OnEarnedChanged += handler;
            handler(_earned);
        }

        private int _earned = 0;
        public int Earned
        {
            get { return _earned; }
            set
            {
                _earned = value;
                _OnCollectedChanged(_earned);
            }
        }


        #endregion


        public void Awake()
        {
            App.Cache.gameStateManager.OnGameStateChanged(OnGameStateChanged);
        }

        // Commit Money to profile, if the game ended, or we are switching
        private void OnGameStateChanged(GameStateMachine.GameState state)
        {
            if(state == GameStateMachine.GameState.End || state == GameStateMachine.GameState.Switching)
            {
                TryCommitPools();
            }
        }

        public void AddPickup(int amount)
        {
            if (amount <= 0) { return; }
            Collected += amount;
        }


        public void AddRoundEnd(int amount)
        {
            if (amount <= 0) { return; }
            Earned += amount;
        }


        /// <summary>
        /// Commits the accumulated money pools to the PlayerProfile
        /// </summary>
        /// <returns></returns>
        public bool TryCommitPools()
        {
            if (!CanDoTransactions)
            {
                return false;
            }

            App.Cache.playerProfile.Currency += Math.Abs(Collected) + Math.Abs(Earned);
            Collected = 0;
            Earned = 0;

            return true;
        }
    }
}
