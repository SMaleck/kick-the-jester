using Assets.Source.App.Persistence;
using Assets.Source.Behaviours.Jester;
using System.Linq;
using UniRx;

namespace Assets.Source.Behaviours.GameLogic.Components
{
    public class GameStatsRecorder : AbstractComponent<GameLogicContainer>
    {        
        private readonly PlayerProfileContext playerProfile;
        private readonly JesterContainer jester;

        private float currentDistance = 0;

        public GameStatsRecorder(GameLogicContainer owner, PlayerProfileContext playerProfile, JesterContainer jester)
            : base(owner)
        {            
            this.playerProfile = playerProfile;

            jester.DistanceProperty
                  .Subscribe(RecordDistance)
                  .AddTo(owner);

            owner.StateProperty
                 .Where(e => e.Equals(GameState.End))
                 .Subscribe(_ => OnGameEnd())
                 .AddTo(owner);
        }

        private void RecordDistance(float distance)
        {
            currentDistance = distance;
        }

        private void OnGameEnd()
        {
            // Increment round counter
            playerProfile.Stats.RoundsPlayed++;

            // Record best distance if achieved
            if (currentDistance > playerProfile.Stats.BestDistance)
            {
                playerProfile.Stats.BestDistance = (int)(currentDistance);
            }
        }
    }
}
