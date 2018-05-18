using Assets.Source.AppKernel;
using Assets.Source.Repositories;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    public class CurrencyManager : MonoBehaviour
    {        
        private void Awake()
        {          
            App.Cache.RepoRx.GameStateRepository.StateProperty
                                                .TakeUntilDestroy(this)
                                                .Where(e => e.Equals(GameState.End))
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
            App.Cache.jester.CollectedCurrency += amount;
        }


        public void AddRoundEnd(int amount)
        {
            if (amount <= 0) { return; }
            App.Cache.jester.EarnedCurrency += amount;
        }


        /// <summary>
        /// Commits the accumulated money pools to the PlayerProfile
        /// </summary>
        /// <returns></returns>
        public bool TryCommitPools()
        {
            Kernel.PlayerProfileService.Currency += Math.Abs(App.Cache.jester.CollectedCurrency) + Math.Abs(App.Cache.jester.EarnedCurrency);
            App.Cache.jester.CollectedCurrency = 0;
            App.Cache.jester.EarnedCurrency = 0;

            return true;
        }
    }
}
