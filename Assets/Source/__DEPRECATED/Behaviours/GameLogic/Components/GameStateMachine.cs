using Assets.Source.App;
using Assets.Source.Behaviours.Jester;
using System.Linq;
using UniRx;

namespace Assets.Source.Behaviours.GameLogic.Components
{
    public class GameStateMachine : AbstractComponent<GameLogicContainer>
    {
        private readonly ReactiveProperty<GameState> stateProperty;
        private readonly AppState appState;
        private readonly UserControl userControl;
        private readonly JesterContainer jester;

        private GameState previousState;

        #region REACTIVE PROPERTIES

        public GameState State
        {
            get { return stateProperty.Value; }
            set
            {
                stateProperty.Value = value;
                IsPaused = value.Equals(GameState.Paused);
            }
        }

        public BoolReactiveProperty IsPausedProperty = new BoolReactiveProperty(false);
        public bool IsPaused
        {
            get { return IsPausedProperty.Value; }
            set { IsPausedProperty.Value = value; }
        }

        #endregion


        public GameStateMachine(GameLogicContainer owner, ReactiveProperty<GameState> stateProperty, AppState appState, UserControl userControl, JesterContainer jester)
            : base(owner)
        {
            this.stateProperty = stateProperty;
            this.appState = appState;
            this.userControl = userControl;
            this.jester = jester;

            State = GameState.Launch;
            previousState = State;

            jester.IsLandedProperty
                  .Where(e => e)
                  .Subscribe(_ => ToEnd())
                  .AddTo(owner);

            userControl.OnKick(ToFlight);

            userControl.OnTogglePause(() => { TogglePause(); });


            // Set global paused state
            appState.IsPausedProperty.Value = State.Equals(GameState.Paused);
        }


        public void TogglePause()
        {
            if (State != GameState.Paused)
            {
                previousState = State;
                State = GameState.Paused;
            }
            else
            {
                State = previousState;
            }

            appState.IsPausedProperty.Value = State.Equals(GameState.Paused);
        }


        public void ToFlight()
        {
            if (State != GameState.End && State != GameState.Paused)
            {
                State = GameState.Flight;
            }
        }


        public void ToEnd()
        {
            if (State != GameState.Paused)
            {
                State = GameState.End;
            }
        }
    }
}
