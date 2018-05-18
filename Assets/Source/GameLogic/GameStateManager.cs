using Assets.Source.Repositories;
using UnityEngine;
using UniRx;

namespace Assets.Source.GameLogic
{
    public class GameStateManager : MonoBehaviour
    {        
        public GameStateMachine stateMachine;


        private void Awake()
        {            
            stateMachine = new GameStateMachine(GameState.Launch, App.Cache.RepoRx.GameStateRepository);

            App.Cache.jester.OnLanded.Subscribe(_ => stateMachine.ToEnd()).AddTo(this);

            App.Cache.userControl.OnKick(stateMachine.ToFlight);

            App.Cache.userControl.OnTogglePause(() => { stateMachine.TogglePause(); });
        }
    }
}
