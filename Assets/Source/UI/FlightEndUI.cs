using Assets.Source.App;
using Assets.Source.GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI
{
    public class FlightEndUI : MonoBehaviour
    {
        public Text txtGameOver;

        void Start()
        {
            txtGameOver.text = "";

            // Register for Updates
            Singletons.gameStateManager.AttachForGameState(UpdateUI);
        }

        private void UpdateUI(GameStateMachine.GameStates state)
        {            
            if(state == GameStateMachine.GameStates.End)
            {
                txtGameOver.text = "GAME OVER";
            }
        }
    }
}
