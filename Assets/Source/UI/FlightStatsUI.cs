using Assets.Source.Entities;
using Assets.Source.Structs;
using System;
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
            App.Cache.rxState.AttachForFlightStats(this.UpdateUI);
            App.Cache.playerProfile.AddEventHandler(this.UpdateBestDistance);
        }

        public void UpdateUI(FlightStats stats)
        {
            txtDistance.text = stats.Distance.ToString() + "m";
            txtHeight.text = stats.Height.ToString() + "m";
            txtVelocity.text = Math.Round(stats.Velocity.magnitude, 2).ToString() + "km/h";
        }

        public void UpdateBestDistance(PlayerProfile profile)
        {            
            txtBestDistance.text = profile.BestDistance.ToString() + "m";
        }
    }
}