using Assets.Source.Repositories;

namespace Assets.Source.GameLogic
{
    public class GameStateMachine
    {        
        private GameStateRepository repo;        
        private GameState previousState;


        public GameStateMachine(GameState state, GameStateRepository repo)
        {                        
            this.repo = repo;
            this.repo.State = state;
            previousState = state;
        }


        public void TogglePause(bool isPaused)
        {
            if (isPaused)
            {
                previousState = repo.State;
                repo.State = GameState.Paused;
            }
            else
            {
                repo.State = previousState;
            }
        }


        public void ToFlight()
        {
            if(repo.State != GameState.End && repo.State != GameState.Paused)
            {
                repo.State = GameState.Flight;
            }
        }


        public void ToEnd()
        {
            if (repo.State != GameState.Paused)
            {
                repo.State = GameState.End;
            }
        }
    }
}
