using Assets.Source.App;
using Assets.Source.Behaviours.GameLogic.Components;
using UniRx;

namespace Assets.Source.Behaviours.GameLogic
{
    public enum GameState { Launch, Flight, End, Paused }

    public class GameLogicContainer : AbstractBehaviour
    {
        public ReactiveProperty<GameState> StateProperty = new ReactiveProperty<GameState>();


        #region COMPONENTS

        public GameStateMachine gameStateMachine { get; private set; }
        public GameStatsRecorder gameStatsRecorder { get; private set; }
        public CurrencyRecorder currencyRecorder { get; private set; }

        #endregion

        private void Awake()
        {            
            gameStateMachine = new GameStateMachine(this, StateProperty, Kernel.AppState, App.Cache.userControl, App.Cache.Jester);
            gameStatsRecorder = new GameStatsRecorder(this, Kernel.PlayerProfileService, App.Cache.Jester);
            currencyRecorder = new CurrencyRecorder(this, Kernel.PlayerProfileService);
        }
    }
}
