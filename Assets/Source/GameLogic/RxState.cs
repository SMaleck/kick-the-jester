using Assets.Source.Entities.Behaviours;
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
        public delegate void FlightStatEventHandler(FlightStats stats);        

        #endregion


        #region EVENT HANDLERS

        private event GameStateEventHandler OnGameStateChanged = delegate { };
        private event FlightStatEventHandler OnFlightStatsChanged = delegate { };
        private event IntValueEventHandler OnRelativeKickForceChanged = delegate { };

        #endregion

        public void AttachForGameState(GameStateEventHandler handler)
        {
            OnGameStateChanged += handler;
        }

        public void AttachForFlightStats(FlightStatEventHandler handler)
        {
            OnFlightStatsChanged += handler;
        }

        public void AttachForRelativeKickForce(IntValueEventHandler handler)
        {
            OnRelativeKickForceChanged += handler;
        }


        #region START

        // Components, which we poll for data
        private FlightRecorder flightRecorder;
        private KickForceManager kickForceManager;


        public void Start()
        {
            flightRecorder = App.Cache.jester.GetComponent<FlightRecorder>();
            kickForceManager = App.Cache.jester.GetComponent<KickForceManager>();
        }

        #endregion


        #region UPDATE LOOPS

        public void LateUpdate()
        {
            OnFlightStatsChanged(flightRecorder.GetFlightStats());
            OnRelativeKickForceChanged(kickForceManager.GetRelativeKickForce());
        }

        #endregion
    }
}
