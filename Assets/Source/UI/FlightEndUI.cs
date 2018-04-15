using Assets.Source.App;
using Assets.Source.GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI
{
    public class FlightEndUI : MonoBehaviour
    {
        public GameObject Panel;
        void Start()
        {
            Panel.SetActive(false);

            // Register for Updates
            App.Cache.gameStateManager.AttachForGameState(this.UpdateUI);
        }

        private void UpdateUI(GameStateMachine.GameState state)
        {            
            if(state == GameStateMachine.GameState.End)
            {
                Panel.SetActive(true);
            }
        }
    }
}
