using Assets.Source.App;
using Assets.Source.GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI
{
    public class FlightEndUI : MonoBehaviour
    {
        public Text txtGameOver;
        public Button shopButton;

        void Start()
        {
            txtGameOver.text = "";

            // Register for Updates
            Singletons.gameStateManager.AttachForGameState(UpdateUI);
        }

        private void UpdateUI(GameStateMachine.GameState state)
        {            
            if(state == GameStateMachine.GameState.End)
            {
                txtGameOver.text = "GAME OVER";
                shopButton.interactable = true;
            }
        }
    }
}
