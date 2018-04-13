using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Source.GameLogic
{
    public class GameStateMachine
    {
        public enum GameState { Launch, Flight, End, Paused }

        private GameState StateBeforePause;
        public GameState State { get; private set; }
        

        public GameStateMachine()
        {
            State = GameState.Launch;
            StateBeforePause = State;
        }


        public void TogglePause(bool isPaused)
        {
            if (isPaused)
            {
                StateBeforePause = State;
                State = GameState.Paused;
            }
            else
            {
                State = StateBeforePause;
            }
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
