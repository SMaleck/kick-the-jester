using Assets.Source.App;
using UniRx;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    public enum GameState { Launch, Flight, End, Paused }

    public class GameStateMachine : MonoBehaviour
    {
        #region REACTIVE PROPERTIES

        public ReactiveProperty<GameState> StateProperty = new ReactiveProperty<GameState>();
        public GameState State
        {
            get { return StateProperty.Value; }
            set
            {
                StateProperty.Value = value;
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

        
        private GameState previousState;

        private void Awake()
        {
            State = GameState.Launch;
            previousState = State;

            App.Cache.jester.IsLandedProperty
                            .Where(e => e)
                            .Subscribe(_ => ToEnd())
                            .AddTo(this);

            App.Cache.userControl.OnKick(ToFlight);

            App.Cache.userControl.OnTogglePause(() => { TogglePause(); });


            // Set global paused state
            Kernel.AppState.IsPausedProperty.Value = State.Equals(GameState.Paused);
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

            Kernel.AppState.IsPausedProperty.Value = State.Equals(GameState.Paused);
        }


        public void ToFlight()
        {
            if(State != GameState.End && State != GameState.Paused)
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
