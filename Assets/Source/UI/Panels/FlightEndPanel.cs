using Assets.Source.App;
using Assets.Source.Behaviours.GameLogic;
using Assets.Source.UI.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class FlightEndPanel : AbstractPanel
    {
        [Header("Panel Properties")]
        [SerializeField] Button retryButton;
        [SerializeField] Button shopButton;

        [SerializeField] Text distance;
        [SerializeField] Text bestDistance;
        [SerializeField] GameObject newBestLabel;

        [SerializeField] RectTransform currencyContainer;
        [SerializeField] GameObject pfCurrencyItem;
        [SerializeField] Text currency;

        private double activationDelay = 0.7;
        private float initialBestDistance;

        public override void Setup()
        {
            base.Setup();

            App.Cache.GameLogic.StateProperty
                               .Where(e => e.Equals(GameState.End))
                               .Subscribe(_ => OnFlightEndDelayed())
                               .AddTo(this);

            retryButton.OnClickAsObservable().Subscribe(_ => OnRetryClicked()).AddTo(this);
            shopButton.OnClickAsObservable().Subscribe(_ => ShowPanelByName("PF_Panel_Shop")).AddTo(this);
            
            App.Cache.Jester.DistanceProperty
                            .Subscribe(x => { distance.text = x.ToMeters() + "m"; })
                            .AddTo(this);

            Kernel.PlayerProfileService.RP_BestDistance
                                       .SubscribeToText(bestDistance, e => string.Format("{0}m", e.ToMeters()))
                                       .AddTo(this);
              
            currency.text = Kernel.PlayerProfileService.Currency.ToString();

            // Setup new Best Distance Marker
            initialBestDistance = Kernel.PlayerProfileService.BestDistance;

            Kernel.PlayerProfileService.RP_BestDistance
                                       .Subscribe(e => { newBestLabel.SetActive(e > initialBestDistance); })
                                       .AddTo(this);
        }


        private void OnFlightEndDelayed()
        {
            Observable.Timer(TimeSpan.FromSeconds(activationDelay)).Subscribe(_ => OnFlightEnd()).AddTo(this);
        }


        private void OnFlightEnd()
        {
            IDictionary<string, int> currencyResults = App.Cache.GameLogic.currencyRecorder.GetResults();
            List<CurrencyItem> currencyItems = new List<CurrencyItem>();

            Rect pfRect = pfCurrencyItem.GetComponent<RectTransform>().rect;

            // Create currency Items
            int index = 0;
            LTSeq ltSeq = LeanTween.sequence();

            foreach (string key in currencyResults.Keys)
            {
                GameObject go = GameObject.Instantiate(pfCurrencyItem, currencyContainer, false);              

                currencyItems.Add(go.GetComponent<CurrencyItem>());

                var ci = currencyItems.Last();
                ci.Label = key;
                ci.Value = "";

                // Reset randomized rect values
                ci.GetComponent<RectTransform>().rect.Set(pfRect.x, pfRect.y, pfRect.width, pfRect.height);

                // Space items out vertically
                Vector3 pos = ci.transform.localPosition;
                ci.transform.localPosition = new Vector3(pos.x, pos.y + (-65 * index), pos.z);

                index++;
            }

            // Setup LeanTween sequence
            AddCurrencyTweens(ltSeq, currencyResults, currencyItems);

            // Set tweening for total amount
            float flightCurrency = currencyResults.Values.Sum();
            int totalCurrency = Kernel.PlayerProfileService.Currency;
            ltSeq.append(LeanTween.value(this.gameObject, (float value) => { currency.text = Mathf.RoundToInt(value).ToString(); }, totalCurrency - flightCurrency, totalCurrency, 1f));

            // Activate Panel            
            Show();
        }


        private void AddCurrencyTweens(LTSeq ltSeq, IDictionary<string, int> results, List<CurrencyItem> items)
        {
            foreach(CurrencyItem ci in items)
            {
                ltSeq.append(LeanTween.value(this.gameObject, (float value) => { ci.Value = Mathf.RoundToInt(value).ToString(); }, 0f, results[ci.Label], 1f));
            }
        }


        private void OnRetryClicked()
        {
            Kernel.SceneTransitionService.ToGame();
        }
    }
}
