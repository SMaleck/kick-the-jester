using System;
using Assets.Source.App;
using Assets.Source.Behaviours.Jester;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Components
{
    public class KickForceUI : MonoBehaviour
    {
        Slider slider;

        void Start()
        {            
            slider = gameObject.GetComponentInChildren<Slider>();
            slider.maxValue = 100;

            // Register for Updates
            App.Cache.rxState.AttachForRelativeKickForce(this.UpdateUI);
            App.Cache.jester.GetComponent<FlightRecorder>().OnStartedFlight(HideUI);
        }

        private void HideUI()
        {
            gameObject.SetActive(false);
        }

        public void UpdateUI(int value)
        {                        
            slider.value = value;
        }
    }
}