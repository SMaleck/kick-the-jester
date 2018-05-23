using Assets.Source.App.Storage;
using Assets.Source.Behaviours.Jester;
using System;
using System.Linq;
using UniRx;

namespace Assets.Source.Behaviours.GameLogic.Components
{
    public class GameStatsRecorder : AbstractComponent<GameLogicContainer>
    {        
        private readonly PlayerProfileService playerProfileService;
        private readonly JesterContainer jester;

        private float currentDistance = 0;

        public GameStatsRecorder(GameLogicContainer owner, PlayerProfileService playerProfileService, JesterContainer jester)
            : base(owner)
        {            
            this.playerProfileService = playerProfileService;

            jester.DistanceProperty
                  .Subscribe(RecordDistance)
                  .AddTo(owner);

            owner.StateProperty
                 .Where(e => e.Equals(GameState.End))
                 .Subscribe(OnGameStateChange)
                 .AddTo(owner);
        }

        private void RecordDistance(float distance)
        {
            currentDistance = distance;
        }

        private void OnGameStateChange(GameState state)
        {
            if (state == GameState.End && currentDistance > playerProfileService.BestDistance)
            {
                playerProfileService.BestDistance = (int)Math.Round(currentDistance);
            }
        }
    }
}
