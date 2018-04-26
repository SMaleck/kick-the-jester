using Assets.Source.Repositories;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    public class GameStateManager : MonoBehaviour
    {        
        public GameStateMachine stateMachine;


        private void Awake()
        {            
            stateMachine = new GameStateMachine(GameState.Launch, App.Cache.RepoRx.GameStateRepository);

            App.Cache.RepoRx.JesterStateRepository
                            .OnLanded(stateMachine.ToEnd);

            App.Cache.userControl.AttachForKick(stateMachine.ToFlight);

            App.Cache.userControl.AttachForPause(this.OnPauseGame);            
        }


        #region EVENT HANDLERS

        // Pauses the Game on a global level
        private void OnPauseGame(bool isPaused)
        {
            stateMachine.TogglePause(isPaused);            

            Time.timeScale = isPaused ? 0 : 1;
        }

        #endregion
    }
}
