﻿using Assets.Source.App;
using Assets.Source.GameLogic;
using Assets.Source.Repositories;
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
            // Flight Stats
            App.Cache.RepoRx.JesterStateRepository
                            .DistanceProperty
                            .TakeUntilDestroy(this)
                            .SubscribeToText(txtDistance);

            App.Cache.RepoRx.JesterStateRepository
                            .DistanceProperty
                            .TakeUntilDestroy(this)
                            .Subscribe((float value) => { UpdateText(MathUtil.UnitsToMeters(value), txtDistance, "m"); });

            App.Cache.RepoRx.JesterStateRepository
                            .HeightProperty
                            .TakeUntilDestroy(this)
                            .Subscribe((float value) => { UpdateText(MathUtil.UnitsToMeters(value), txtHeight, "m"); });

            App.Cache.RepoRx.JesterStateRepository
                            .VelocityProperty
                            .TakeUntilDestroy(this)
                            .Subscribe((Vector2 value) => { UpdateText(Mathf.Ceil(value.magnitude), txtVelocity, "kmh"); });

            // Currency
            App.Cache.RepoRx.JesterStateRepository
                            .CollectedCurrencyProperty
                            .TakeUntilDestroy(this)
                            .Subscribe((int value) => { UpdateText(value, txtCollectedCurrency, "G"); });

            App.Cache.playerProfile.currencyProperty
                            .TakeUntilDestroy(this)
                            .Subscribe((int value) => { UpdateText(value, txtTotalCurrency, "G"); });

            // Distance
            App.Cache.playerProfile.bestDistanceProperty
                            .TakeUntilDestroy(this)
                            .Subscribe((int value) => { UpdateText(value, txtBestDistance, "m"); });
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