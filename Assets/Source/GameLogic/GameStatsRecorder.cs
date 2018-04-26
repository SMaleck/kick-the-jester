using Assets.Source.App;
using Assets.Source.Repositories;
using UniRx;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    public class GameStatsRecorder : MonoBehaviour
    {
        private int currentDistance = 0;

        // Use this for initialization
        void Start()
        {
            App.Cache.JesterState.DistanceProperty
                                 .TakeUntilDestroy(this)
                                 .Subscribe(RecordDistance);

            App.Cache.gameStateManager.OnGameStateChanged(this.OnGameStateChange);
        }

        private void RecordDistance(float distance)
        {
            currentDistance = MathUtil.UnitsToMeters(distance);
        }

        private void OnGameStateChange(GameStateMachine.GameState state)
        {
            if (state == GameStateMachine.GameState.End && currentDistance > App.Cache.playerProfile.BestDistance)
            {
                App.Cache.playerProfile.BestDistance = currentDistance;
            }
        }
    }
}