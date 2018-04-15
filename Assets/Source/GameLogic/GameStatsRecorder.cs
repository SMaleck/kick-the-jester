using Assets.Source.App;
using Assets.Source.Entities;
using Assets.Source.GameLogic;
using Assets.Source.Structs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatsRecorder : MonoBehaviour {

    private PlayerProfile playerProfile;
    private int currentDistance = 0;

	// Use this for initialization
	void Start () {

        GameObjectPool.gameStateManager.AttachForFlightStats(OnFlightStatsChange);
        GameObjectPool.gameStateManager.AttachForGameState(OnGameStateChange);
        GameObjectPool.playerProfile.AddEventHandler(OnProfileLoaded);
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
