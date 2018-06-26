using Assets.Source.App;
using Assets.Source.Behaviours.GameLogic;
using Assets.Source.Models;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Source.Behaviours
{
    public class UserControl : MonoBehaviour, IPointerDownHandler
    {
        // Internal Toggles
        private bool kickEnabled = false;        


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


        private void Start()
        {
            // Attach this to the camera, so the raycast works correctly
            this.transform.SetParent(App.Cache.MainCamera.UCamera.transform);

            App.Cache.Kernel.AppState.IsPausedProperty
                           .Subscribe((bool value) => { kickEnabled = !value; })
                           .AddTo(this);

            App.Cache.GameLogic.StateProperty
                               .Subscribe((GameState state) => { kickEnabled = !state.Equals(GameState.Paused) && !state.Equals(GameState.End); })
                               .AddTo(this);            
        }


        void Update()
        {
            
            if (kickEnabled && Input.GetButtonDown("Kick"))
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
                case -1:
                case 0:
                    if (kickEnabled)
                    {
                        _OnKick();
                    }                    
                    break;

                default:                    
                    break;
            }
        }


        public void TooglePause()
        {
            _OnTogglePause();
        }
    }
}

