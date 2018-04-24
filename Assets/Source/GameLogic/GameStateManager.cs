using Assets.Source.Behaviours.Jester;
using Assets.Source.Models;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    public class GameStateManager : MonoBehaviour
    {
        #region GAME STATE

        public GameStateMachine GameState = new GameStateMachine();

        // Delegate Definition for GameState Event
        public delegate void GameStateEventHandler(GameStateMachine.GameState state);

        // GameState Event Handling
        private event GameStateEventHandler _OnGameStateChanged = delegate { };
        public void OnGameStateChanged(GameStateEventHandler handler)
        {
            _OnGameStateChanged += handler;
            handler(GameState.State);
        }

        #endregion


        private void Awake()
        {
            App.Cache.JesterState.OnLanded(OnLanded);

            App.Cache.userControl.AttachForKick(this.OnKick);
            App.Cache.userControl.AttachForPause(this.OnPauseGame);            
        }


        #region EVENT HANDLERS

        // Changes state when the screen manager starts loading
        private void OnScreenSwitching()
        {
            GameState.ToSwitching();
            _OnGameStateChanged(GameState.State);
        }


        // Change GameState when the Jester is landed
        private void OnLanded()
        {
            if(GameState.State == GameStateMachine.GameState.Flight)
            {
                GameState.ToEnd();
                _OnGameStateChanged(GameState.State);
            }
        }


        // Switches GameState to Flight Mode
        private void OnKick()
        {
            GameState.ToFlight();
            _OnGameStateChanged(GameState.State);
        }


        // Pauses the Game on a global level
        private void OnPauseGame(bool isPaused)
        {
            GameState.TogglePause(isPaused);
            _OnGameStateChanged(GameState.State);

            Time.timeScale = isPaused ? 0 : 1;
        }

        #endregion
    }
}
