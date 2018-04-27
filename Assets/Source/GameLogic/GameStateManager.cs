using Assets.Source.Repositories;
using UnityEngine;

namespace Assets.Source.GameLogic
{
    public class GameStateManager : MonoBehaviour
    {
        private bool IsPaused;
        public GameStateMachine stateMachine;


        private void Awake()
        {            
            stateMachine = new GameStateMachine(GameState.Launch, App.Cache.RepoRx.GameStateRepository);

            App.Cache.RepoRx.JesterStateRepository
                            .OnLanded(stateMachine.ToEnd);

            App.Cache.userControl.OnKick(stateMachine.ToFlight);

            App.Cache.userControl.OnTogglePause(this.OnTogglePause);
        }


        // Pauses the Game on a global level
        private void OnTogglePause()
        {
            bool isPaused = stateMachine.TogglePause();

            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}
