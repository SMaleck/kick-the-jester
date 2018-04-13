using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Source.GameLogic
{
    public class GameStateMachine
    {
        public enum GameStates { Launch, Flight, End, Paused }

        private GameStates StateBeforePause;
        public GameStates State { get; private set; }
        

        public GameStateMachine()
        {
            State = GameStates.Launch;
            StateBeforePause = State;
        }


        public void TogglePause(bool isPaused)
        {
            if (isPaused)
            {
                StateBeforePause = State;
                State = GameStates.Paused;
            }
            else
            {
                State = StateBeforePause;
            }
        }


        public void ToFlight()
        {
            if(State != GameStates.End && State != GameStates.Paused)
            {
                State = GameStates.Flight;
            }
        }


        public void ToEnd()
        {
            if (State != GameStates.Paused)
            {
                State = GameStates.End;
            }
        }

    }
}
