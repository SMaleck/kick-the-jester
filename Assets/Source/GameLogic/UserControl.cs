using UnityEngine;
using Assets.Source.App;
using System;
using UnityEngine.EventSystems;

namespace Assets.Source.GameLogic
{
    public class UserControl : MonoBehaviour, IPointerDownHandler
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
            
            if (Input.GetButtonDown("Kick"))
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
        
        // Handles both touch and clicks
        public void OnPointerDown(PointerEventData eventData)
        {
            switch (eventData.pointerId)
            {
                case 13:
                    // well done. 14 fingers touch! ;D
                    App.Cache.playerProfile.Currency += 1000;
                    break;
                case -1:
                case 0:
                    InputKickHandler();
                    break;
                default:
                    // nothing yet.
                    break;
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

