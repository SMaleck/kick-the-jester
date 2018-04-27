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
            App.Cache.RepoRx.JesterStateRepository.DistanceProperty
                                 .TakeUntilDestroy(this)
                                 .Subscribe(RecordDistance);

            App.Cache.RepoRx.GameStateRepository.StateProperty
                                                .TakeUntilDestroy(this)
                                                .Where(e => e.Equals(GameState.End))
                                                .Subscribe(OnGameStateChange);
        }

        private void RecordDistance(float distance)
        {
            currentDistance = MathUtil.UnitsToMeters(distance);
        }

        private void OnGameStateChange(GameState state)
        {
            if (state == GameState.End && currentDistance > App.Cache.playerProfile.BestDistance)
            {
                App.Cache.playerProfile.BestDistance = currentDistance;
            }
        }
    }
}