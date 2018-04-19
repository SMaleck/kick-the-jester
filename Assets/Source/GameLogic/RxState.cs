using Assets.Source.Behaviours.Jester;
using Assets.Source.Models;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    /// <summary>
    /// The global reactive data channel for the game.    
    /// </summary>
    public class RxState : MonoBehaviour
    {
        #region DELEGATE DEFINITIONS

        public delegate void GameStateEventHandler(GameStateMachine.GameState state);        

        #endregion


        #region EVENT HANDLERS

        private event GameStateEventHandler OnGameStateChanged = delegate { };
        private event IntValueEventHandler OnRelativeKickForceChanged = delegate { };

        #endregion

        public void AttachForGameState(GameStateEventHandler handler)
        {
            OnGameStateChanged += handler;
        }

        public void AttachForRelativeKickForce(IntValueEventHandler handler)
        {
            OnRelativeKickForceChanged += handler;
        }


        #region START

        // Components, which we poll for data        
        private KickForceManager kickForceManager;


        public void Start()
        {            
            kickForceManager = App.Cache.jester.GetComponent<KickForceManager>();
        }

        #endregion


        #region UPDATE LOOPS

        public void LateUpdate()
        {            
            OnRelativeKickForceChanged(kickForceManager.GetRelativeKickForce());
        }

        #endregion
    }
}
