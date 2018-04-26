using Assets.Source.Repositories;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    public class GameStateManager : MonoBehaviour
    {
        private RepoRx repos;
        public GameStateMachine stateMachine = new GameStateMachine();


        private void Awake()
        {
            repos = App.Cache.RepoRx;

            App.Cache.RepoRx.JesterStateRepository.OnLanded(OnLanded);

            App.Cache.userControl.AttachForKick(this.OnKick);
            App.Cache.userControl.AttachForPause(this.OnPauseGame);            
        }


        #region EVENT HANDLERS

        // Changes state when the screen manager starts loading
        private void OnScreenSwitching()
        {
            stateMachine.ToSwitching();
            UpdateStateInRepo();
        }


        // Change GameState when the Jester is landed
        private void OnLanded()
        {
            stateMachine.ToEnd();
            UpdateStateInRepo();
        }


        // Switches GameState to Flight Mode
        private void OnKick()
        {
            stateMachine.ToFlight();
            UpdateStateInRepo();
        }


        // Pauses the Game on a global level
        private void OnPauseGame(bool isPaused)
        {
            stateMachine.TogglePause(isPaused);
            UpdateStateInRepo();

            Time.timeScale = isPaused ? 0 : 1;
        }


        private void UpdateStateInRepo()
        {
            repos.GameStateRepository.State = stateMachine.State;
        }

        #endregion
    }
}
