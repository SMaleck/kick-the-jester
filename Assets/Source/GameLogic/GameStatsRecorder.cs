using Assets.Source.Entities;
using Assets.Source.Structs;
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

            App.Cache.rxState.AttachForFlightStats(this.OnFlightStatsChange);
            App.Cache.gameStateManager.AttachForGameState(this.OnGameStateChange);
            App.Cache.playerProfile.AddEventHandler(this.OnProfileLoaded);
        }

        private void OnFlightStatsChange(FlightStats stats)
        {
            currentDistance = stats.Distance;
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