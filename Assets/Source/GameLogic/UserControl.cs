using UnityEngine;
using Assets.Source.App;

namespace Assets.Source.GameLogic
{
    public class UserControl : MonoBehaviour
    {
        /* ----------------------------- EVENT HANDLING ----------------------------- */
        #region EVENT HANDLING

        public delegate void InputEventHandler();
        public delegate void ToggleEventHandler(bool State);

        public event InputEventHandler InputKickHandler = delegate { };
        public event ToggleEventHandler InputPauseHandler = delegate { };

        public void AttachForKick(InputEventHandler handler)
        {            
            InputKickHandler += handler;
        }

        // ONly GameStateManager should attach here, everything else should listen there
        public void AttachForPause(ToggleEventHandler handler)
        {
            InputPauseHandler += handler;
        }

        #endregion

        /* ----------------------------- ----------------------------- */

        private bool IsPaused = false;
        private bool CanGoToShop = false;

        void Start()
        {
            App.Cache.gameStateManager.AttachForGameState(this.OnGameStateChange);
        }

        void Update()
        {
            
            if (Input.GetButtonDown("Kick") || Input.touchCount > 0)
            {                
                InputKickHandler();
            }

            if (Input.GetButtonDown("Pause"))
            {
                TooglePauseGame();
            }

            if (Input.GetButtonDown("Buy"))
            {
                ShowShop();
            }
        }

        /* ----------------------------- SHOP ----------------------------- */
        #region SHOP

        public void ShowShop()
        {
            if (CanGoToShop)
            {
                App.Cache.screenManager.ShowShop();
            }
        }

        public void OnGameStateChange(GameStateMachine.GameState gameState)
        {
            if (gameState == GameStateMachine.GameState.End)
            {
                // Allow going to the shop only after the game is over
                CanGoToShop = true;
            }
        }

        #endregion

        /* ----------------------------- PAUSE GAME ----------------------------- */
        #region PAUSE GAME

        public void PauseGame()
        {
            IsPaused = true;
            InputPauseHandler(IsPaused);
        }

        public void UnPauseGame()
        {
            IsPaused = false;
            InputPauseHandler(IsPaused);
        }

        public void TooglePauseGame()
        {
            IsPaused = !IsPaused;
            InputPauseHandler(IsPaused);
        }

        #endregion
    }
}

