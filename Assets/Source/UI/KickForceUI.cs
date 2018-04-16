﻿using Assets.Source.App;
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
        }

        public void UpdateUI(int value)
        {                        
            slider.value = value;
        }
    }
}