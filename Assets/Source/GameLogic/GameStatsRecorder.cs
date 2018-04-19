using Assets.Source.Behaviours.Jester;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    public class GameStatsRecorder : MonoBehaviour
    {
        private PlayerProfile playerProfile;
        private int currentDistance = 0;

        // Use this for initialization
        void Start()
        {
            App.Cache.jester.GetComponent<FlightRecorder>().OnDistanceChanged(RecordDistance);
            
            App.Cache.gameStateManager.OnGameStateChanged(this.OnGameStateChange);
            App.Cache.playerProfile.OnProfileLoaded(this.OnProfileLoaded);
        }

        private void RecordDistance(int distance)
        {
            currentDistance = distance;
        }

        private void OnProfileLoaded(PlayerProfile profile)
        {
            playerProfile = profile;
        }

        private void OnGameStateChange(GameStateMachine.GameState state)
        {
            if (state == GameStateMachine.GameState.End && currentDistance > playerProfile.BestDistance)
            {
                playerProfile.BestDistance = currentDistance;
            }
        }
    }
}