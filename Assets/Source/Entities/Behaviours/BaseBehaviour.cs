using Assets.Source.GameLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Entities.Behaviours
{
    public class BaseBehaviour : MonoBehaviour
    {
        private BaseEntity _Entity;
        public BaseEntity Entity
        {
            get
            {
                if(_Entity == null)
                {
                    _Entity = gameObject.GetComponent<BaseEntity>();
                }

                return _Entity;
            }
        }

        protected bool IsActive = true;
        private List<GameStateMachine.GameState> InactiveStates = new List<GameStateMachine.GameState>();

        protected void DeactivateOnStates(List<GameStateMachine.GameState> InactiveStates)
        {
            this.InactiveStates = InactiveStates;
            App.Cache.gameStateManager.AttachForGameState(this.OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateMachine.GameState state)
        {
            IsActive = !InactiveStates.Contains(state);
        }
    }
}
