using Assets.Source.App;
using Assets.Source.Structs;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI
{
    public class FlightStatsUI : MonoBehaviour
    {        
        public Text txtDistance;
        public Text txtHeight;
        public Text txtVelocity;

        void Start()
        {
            // Register for Updates
            Singletons.gameStateManager.AttachForFlightStats(UpdateUI);
        }


        public void UpdateUI(FlightStats stats)
        {
            txtDistance.text = stats.Distance.ToString();
            txtHeight.text = stats.Height.ToString();
            txtVelocity.text = stats.Velocity.ToString();
        }
    }
}