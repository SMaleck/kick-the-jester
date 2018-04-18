using UnityEngine;
using Assets.Source.App;
using System;

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
            App.Cache.gameStateManager.OnGameStateChanged(this.OnGameStateChange);
        }

        void Update()
        {
            
            if (IsKickInput())
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

        private bool IsKickInput()
        {
            bool touchDown = false;
            if (Input.touchCount > 0)
            {
                // The first one is enough for our purposes
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    touchDown = true;
                }
            }
            return Input.GetButtonDown("Kick") || touchDown;
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

