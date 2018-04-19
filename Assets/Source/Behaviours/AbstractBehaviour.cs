using Assets.Source.GameLogic;
using System.Collections.Generic;
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
        private List<GameStateMachine.GameState> InactiveStates = new List<GameStateMachine.GameState>();

        protected void DeactivateOnStates(List<GameStateMachine.GameState> InactiveStates)
        {
            this.InactiveStates = InactiveStates;
            App.Cache.gameStateManager.OnGameStateChanged(this.OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateMachine.GameState state)
        {
            IsActive = !InactiveStates.Contains(state);
        }
    }
}
