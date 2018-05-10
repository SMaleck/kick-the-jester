using Assets.Source.Models;
using Assets.Source.Repositories;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Source.GameLogic
{
    public class UserControl : MonoBehaviour, IPointerDownHandler
    {
        // Internal Toggles
        private bool canKick = false;        


        // INPUT EVENT: Kick Action
        private event NotifyEventHandler _OnKick = delegate { };
        public void OnKick(NotifyEventHandler handler)
        {
            _OnKick += handler;
        }


        // INPUT EVENT: Toggle Pause Action
        private event NotifyEventHandler _OnTogglePause = delegate { };
        public void OnTogglePause(NotifyEventHandler handler)
        {
            _OnTogglePause += handler;
        }


        void Start()
        {
            App.Cache.RepoRx.GameStateRepository.StateProperty
                                                .TakeUntilDestroy(this)                                                
                                                .Subscribe(OnGameStateChanged);
        }


        void Update()
        {
            
            if (canKick && Input.GetButtonDown("Kick"))
            {
                _OnKick();
            }

            if (Input.GetButtonDown("Pause"))
            {
                _OnTogglePause();
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
                    _OnKick();
                    break;

                default:                    
                    break;
            }
        }


        // Handles Game State changing
        public void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Launch:
                case GameState.Flight:
                    canKick = true;
                    break;                

                case GameState.Paused:
                    canKick = false;
                    break;

                case GameState.End:
                    canKick = false;
                    break;
            }            
        }


        /* ----------------------------- UI BUTTON METHODS ----------------------------- */
        #region UI BUTTON METHODS


        public void TooglePause()
        {
            _OnTogglePause();
        }

        #endregion
    }
}

