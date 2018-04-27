using UniRx;

namespace Assets.Source.Repositories
{
    public enum GameState { Launch, Flight, End, Paused }

    public class GameStateRepository
    {       
        public ReactiveProperty<GameState> StateProperty = new ReactiveProperty<GameState>();
        public GameState State
        {
            get { return StateProperty.Value; }
            set { StateProperty.Value = value; }
        }


        public IntReactiveProperty RelativeKickForceProperty = new IntReactiveProperty(0);
        public int RelativeKickForce
        {
            get { return RelativeKickForceProperty.Value; }
            set { RelativeKickForceProperty.Value = value; }
        }
    }
}
