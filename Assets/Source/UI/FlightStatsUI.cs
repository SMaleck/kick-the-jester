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
        public Text txtCurrency;

        void Start()
        {
            // Register for Updates
            App.Cache.rxState.AttachForFlightStats(this.UpdateUI);
            App.Cache.playerProfile.OnProfileLoaded(OnProfileLoaded);
        }

        private void OnProfileLoaded(PlayerProfile profile)
        {
            App.Cache.playerProfile.OnBestDistanceChanged(UpdateBestDistance);
            App.Cache.playerProfile.OnCurrencyChanged(UpdateCurrency);            
        }

        public void UpdateUI(FlightStats stats)
        {
            txtDistance.text = stats.Distance.ToString() + "m";
            txtHeight.text = stats.Height.ToString() + "m";
            txtVelocity.text = Math.Round(stats.Velocity.magnitude, 2).ToString() + "km/h";
        }

        private void UpdateBestDistance(int bestDistance)
        {
            if(txtBestDistance != null)
            {
                txtBestDistance.text = bestDistance.ToString() + "m";
            }            
        }

        private void UpdateCurrency(int currency)
        {
            if (txtBestDistance != null)
            {
                txtCurrency.text = currency.ToString() + "G";
            }            
        }
    }
}