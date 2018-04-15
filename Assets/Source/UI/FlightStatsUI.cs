using Assets.Source.App;
using Assets.Source.Entities;
using Assets.Source.Structs;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI
{
    public class FlightStatsUI : MonoBehaviour
    {        
        public Text txtDistance;
        public Text txtBestDistance;
        public Text txtHeight;
        public Text txtVelocity;

        void Start()
        {
            // Register for Updates
            GameObjectPool.gameStateManager.AttachForFlightStats(UpdateUI);
            GameObjectPool.playerProfile.AddEventHandler(UpdateBestDistance);
        }

        public void UpdateUI(FlightStats stats)
        {
            txtDistance.text = stats.Distance.ToString();
            txtHeight.text = stats.Height.ToString();
            txtVelocity.text = stats.Velocity.ToString();
        }

        public void UpdateBestDistance(PlayerProfile profile)
        {
            txtBestDistance.text += profile.BestDistance.ToString();
        }
    }
}