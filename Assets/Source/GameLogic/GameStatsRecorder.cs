using Assets.Source.App;
using Assets.Source.Repositories;
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
            App.Cache.RepoRx.JesterStateRepository.DistanceProperty
                                 .TakeUntilDestroy(this)
                                 .Subscribe(RecordDistance);

            App.Cache.RepoRx.GameStateRepository.StateProperty
                                                .TakeUntilDestroy(this)
                                                .Where(e => e.Equals(GameState.End))
                                                .Subscribe(OnGameStateChange);

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

        private void OnGameStateChange(GameState state)
        {
            if (state == GameState.End && currentDistance > playerProfile.BestDistance)
            {
                playerProfile.BestDistance = currentDistance;
            }
        }
    }
}