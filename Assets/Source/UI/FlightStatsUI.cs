using Assets.Source.Behaviours.Jester;
using Assets.Source.GameLogic;
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
                (int value) => { UpdateText(value, txtDistance, "m"); });

            App.Cache.jester.GetComponent<FlightRecorder>().OnHeightChanged(
                (int value) => { UpdateText(value, txtHeight, "m"); });

            App.Cache.jester.GetComponent<FlightRecorder>().OnVelocityChanged(
                (float value) => { UpdateText(value, txtVelocity, "km/h"); });

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