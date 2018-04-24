using Assets.Source.App;
using Assets.Source.GameLogic;
using UniRx;
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
            App.Cache.JesterState.DistanceProperty.TakeUntilDestroy(this).SubscribeToText(txtDistance);

            App.Cache.JesterState.DistanceProperty
                                 .TakeUntilDestroy(this)
                                 .Subscribe((float value) => { UpdateText(MathUtil.UnitsToMeters(value), txtDistance, "m"); });

            App.Cache.JesterState.HeightProperty
                                 .TakeUntilDestroy(this)
                                 .Subscribe((float value) => { UpdateText(MathUtil.UnitsToMeters(value), txtHeight, "m"); });

            App.Cache.JesterState.VelocityProperty
                                 .TakeUntilDestroy(this)
                                 .Subscribe((Vector2 value) => { UpdateText(Mathf.Ceil(value.magnitude), txtVelocity, "kmh"); });

            // Currency
            App.Cache.JesterState.CollectedCurrencyProperty
                                 .TakeUntilDestroy(this)
                                 .Subscribe((int value) => { UpdateText(value, txtCollectedCurrency, "G"); });
                

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