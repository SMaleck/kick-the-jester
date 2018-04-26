using Assets.Source.Repositories;

namespace Assets.Source.GameLogic
{
    public class GameStateMachine
    {        
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


        public void ToSwitching()
        {
            State = GameState.Switching;
        }
    }
}
