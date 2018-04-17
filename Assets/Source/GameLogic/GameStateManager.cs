using Assets.Source.App;
using Assets.Source.Entities.Behaviours;
using Assets.Source.Structs;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    public class GameStateManager : MonoBehaviour
    {       
        #region EVENT HANDLING

        public delegate void GameStateEventHandler(GameStateMachine.GameState state);
        private event GameStateEventHandler OnGameStateChanged = delegate { };

        public void AttachForGameState(GameStateEventHandler handler)
        {
            OnGameStateChanged += handler;
        }

        #endregion

        // GAME STATE
        public GameStateMachine GameState = new GameStateMachine();

        public void Awake()
        {
            App.Cache.rxState.AttachForFlightStats(CheckIsMoving);
            App.Cache.userControl.AttachForKick(this.OnKick);
            App.Cache.userControl.AttachForPause(this.OnPauseGame);
        }


        private void CheckIsMoving(FlightStats stats)
        {
            if(stats.IsLanded && GameState.State == GameStateMachine.GameState.Flight)
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
