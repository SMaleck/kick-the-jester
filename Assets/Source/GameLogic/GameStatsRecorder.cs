using Assets.Source.App;
using Assets.Source.AppKernel;
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
            App.Cache.jester.DistanceProperty                            
                            .Subscribe(RecordDistance)
                            .AddTo(this);

            App.Cache.GameStateMachine.StateProperty                                                
                                      .Where(e => e.Equals(GameState.End))
                                      .Subscribe(OnGameStateChange)
                                      .AddTo(this);
        }

        private void RecordDistance(float distance)
        {
            currentDistance = distance.ToMeters();
        }

        private void OnGameStateChange(GameState state)
        {
            if (state == GameState.End && currentDistance > Kernel.PlayerProfileService.BestDistance)
            {
                Kernel.PlayerProfileService.BestDistance = currentDistance;
            }
        }
    }
}