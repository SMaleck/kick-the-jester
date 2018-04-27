using Assets.Source.Repositories;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours
{
    public abstract class AbstractBehaviour : MonoBehaviour
    {
        public Transform goTransform
        {
            get
            {
                return gameObject.transform;
            }
        }

        protected bool IsActive = true;
        private List<GameState> InactiveStates = new List<GameState>();

        protected void DeactivateOnStates(List<GameState> InactiveStates)
        {
            this.InactiveStates = InactiveStates;            

            App.Cache.RepoRx.GameStateRepository.StateProperty
                                                .TakeUntilDestroy(this)
                                                .Subscribe(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameState state)
        {
            IsActive = !InactiveStates.Contains(state);
        }
    }
}
