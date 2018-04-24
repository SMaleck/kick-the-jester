using Assets.Source.App;
using UniRx;
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
            App.Cache.JesterState.DistanceProperty
                                 .TakeUntilDestroy(this)
                                 .Subscribe(RecordDistance);

            App.Cache.gameStateManager.OnGameStateChanged(this.OnGameStateChange);
            App.Cache.playerProfile.OnProfileLoaded(this.OnProfileLoaded);
        }

        private void RecordDistance(float distance)
        {
            currentDistance = MathUtil.UnitsToMeters(distance);
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