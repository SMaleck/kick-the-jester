using Assets.Source.Behaviours.Jester;
using Assets.Source.GameLogic;
using Assets.Source.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI
{
    public class FlightStatsUI : MonoBehaviour
    {        
        public Text txtDistance;        
        public Text txtHeight;
        public Text txtVelocity;

        public Text txtBestDistance;
        public Text txtCollectedCurrency;
        public Text txtTotalCurrency;


        void Start()
        {
            // FLight Stats
            App.Cache.jester.GetComponent<FlightRecorder>().OnDistanceChanged(
                (UnitMeasurement value) => { UpdateText((int)value.Meters, txtDistance, "m"); });

            App.Cache.jester.GetComponent<FlightRecorder>().OnHeightChanged(
                (UnitMeasurement value) => { UpdateText((int)value.Meters, txtHeight, "m"); });

            App.Cache.jester.GetComponent<FlightRecorder>().OnVelocityChanged(
                (SpeedMeasurement value) => { UpdateText(Mathf.Ceil(value.Kmh), txtVelocity, "km/h"); });

            // Currency
            App.Cache.currencyManager.OnCollectedChanged(
                (int value) => { UpdateText(value, txtCollectedCurrency, "G"); });

            // Setup listeners when Profile is loaded                        
            App.Cache.playerProfile.OnProfileLoaded(OnProfileLoaded);
        }

        private void OnProfileLoaded(PlayerProfile profile)
        {
            App.Cache.playerProfile.OnBestDistanceChanged(
                (int value) => { UpdateText(value, txtBestDistance, "m"); });

            App.Cache.playerProfile.OnCurrencyChanged(
                (int value) => { UpdateText(value, txtTotalCurrency, "G"); });
        }
        
        private void UpdateText(object value, Text uiElement, string suffix = "")
        {
            if(uiElement != null)
            {
                uiElement.text = value.ToString() + suffix;
            }
        }
        
    }
}