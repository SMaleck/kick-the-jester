using Assets.Source.Repositories;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    public class CurrencyManager : MonoBehaviour
    {
        private JesterStateRepository jesterState;

        #region PROPERTIES

        private bool CanDoTransactions
        {
            get { return App.Cache.playerProfile.IsLoaded; }
        }


        #endregion


        private void Awake()
        {
            jesterState = App.Cache.RepoRx.JesterStateRepository;

            App.Cache.RepoRx.GameStateRepository.StateProperty
                                                .TakeUntilDestroy(this)
                                                .Where(e => e.Equals(GameState.End) || e.Equals(GameState.Switching))
                                                .Subscribe(OnEnd);
        }

        // Commit Money to profile, if the game ended, or we are switching
        private void OnEnd(GameState state)
        {
            TryCommitPools();
        }

        public void AddPickup(int amount)
        {
            if (amount <= 0) { return; }
            jesterState.CollectedCurrency += amount;
        }


        public void AddRoundEnd(int amount)
        {
            if (amount <= 0) { return; }
            jesterState.EarnedCurrency += amount;
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

            App.Cache.playerProfile.Currency += Math.Abs(jesterState.CollectedCurrency) + Math.Abs(jesterState.EarnedCurrency);
            jesterState.CollectedCurrency = 0;
            jesterState.EarnedCurrency = 0;

            return true;
        }
    }
}
