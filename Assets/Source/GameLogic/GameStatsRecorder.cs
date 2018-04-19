﻿using Assets.Source.Models;
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
            App.Cache.gameStateManager.OnGameStateChanged(this.OnGameStateChange);
            App.Cache.playerProfile.OnProfileLoaded(this.OnProfileLoaded);
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