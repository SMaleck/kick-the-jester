using Assets.Source.App;
using Assets.Source.Entities.Components;
using Assets.Source.Structs;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    public class GameStateManager : MonoBehaviour
    {
        #region EVENT HANDLING

        public delegate void GameStateEventHandler(GameStateMachine.GameStates state);
        public delegate void FlightStatEventHandler(FlightStats stats);
        public delegate void ValueEventHandler(int value);

        private event GameStateEventHandler OnGameStateChanged = delegate { };
        private event FlightStatEventHandler OnFlightStatsChanged = delegate { };
        private event ValueEventHandler OnRelativeKickForceChanged = delegate { };

        public void AttachForGameState(GameStateEventHandler handler)
        {
            OnGameStateChanged += handler;
        }

        public void AttachForFlightStats(FlightStatEventHandler handler)
        {
            OnFlightStatsChanged += handler;
        }

        public void AttachForRelativeKickForce(ValueEventHandler handler)
        {
            OnRelativeKickForceChanged += handler;
        }

        #endregion

        // GAME STATE
        public GameStateMachine GameState = new GameStateMachine();

        // Components, which we poll for data
        private FlightRecorder flightRecorder;
        private KickForceManager kickForceManager;


        void Start()
        {
            
            flightRecorder = Singletons.jester.GetComponent<FlightRecorder>();
            kickForceManager = Singletons.jester.GetComponent<KickForceManager>();

            // Register for Pausing the Game
            Singletons.userControl.AttachForKick(OnKick);
            Singletons.userControl.AttachForPause(OnPauseGame);
        }


        void LateUpdate()
        {
            FlightStats stats = flightRecorder.GetFlightStats();
            CheckIsMoving(stats);

            OnFlightStatsChanged(flightRecorder.GetFlightStats());
            OnRelativeKickForceChanged(kickForceManager.GetRelativeKickForce());
        }


        private void CheckIsMoving(FlightStats stats)
        {
            if(stats.IsLanded && GameState.State == GameStateMachine.GameStates.Flight)
            {
                GameState.ToEnd();
                OnGameStateChanged(GameState.State);
            }
        }


        // Switches GameState to Flight Mode
        private void OnKick()
        {
            GameState.ToFlight();
            OnGameStateChanged(GameState.State);
        }


        // Pauses the Game on a global level
        private void OnPauseGame(bool isPaused)
        {
            GameState.TogglePause(isPaused);
            OnGameStateChanged(GameState.State);

            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}
