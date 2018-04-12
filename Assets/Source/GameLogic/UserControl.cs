using UnityEngine;

namespace Assets.Source.GameLogic
{
    public class UserControl : MonoBehaviour
    {
        /* ----------------------------- EVENT HANDLING ----------------------------- */
        #region EVENT HANDLING

        public delegate void InputEventHandler();
        public delegate void ToggleEventHandler(bool State);

        public event InputEventHandler inputKickHandler = delegate { };
        public event ToggleEventHandler inputPauseHandler = delegate { };

        public void AttachForKick(InputEventHandler handler)
        {            
            inputKickHandler += handler;
        }

        public void AttachForPause(ToggleEventHandler handler)
        {
            inputPauseHandler += handler;
        }

        #endregion

        /* ------------------------------------------------------------------------ */

        private bool IsPaused = false;

        void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {                
                inputKickHandler();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                IsPaused = !IsPaused;
                inputPauseHandler(IsPaused);
            }
        }
    }
}

