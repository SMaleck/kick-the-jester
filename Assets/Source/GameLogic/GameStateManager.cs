using Assets.Source.App;
using Assets.Source.Entities.Components;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    public class GameStateManager : MonoBehaviour
    {
        #region EVENT HANDLING

        public delegate void ValueEventHandler(int value);        

        public event ValueEventHandler OnDistanceChanged = delegate { };
        public event ValueEventHandler OnRelativeKickForceChanged = delegate { };

        public void AttachForDistance(ValueEventHandler handler)
        {
            OnDistanceChanged += handler;
        }

        public void AttachForRelativeKickForce(ValueEventHandler handler)
        {
            OnRelativeKickForceChanged += handler;
        }

        #endregion

        private FlightRecorder flightRecorder;
        private KickForceManager kickForceManager;


        void Start()
        {
            flightRecorder = Singletons.jester.GetComponent<FlightRecorder>();
            kickForceManager = Singletons.jester.GetComponent<KickForceManager>();

            // Register for Pausing the Game
            Singletons.userControl.AttachForPause(OnPauseGame);
        }


        void Update()
        {
            OnDistanceChanged(flightRecorder.DistanceMeters);
            OnRelativeKickForceChanged(kickForceManager.GetRelativeKickForce());
        }


        // Pauses the Game on a global level
        public void OnPauseGame(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}
