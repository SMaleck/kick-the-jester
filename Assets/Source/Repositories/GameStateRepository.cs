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
    }
}
