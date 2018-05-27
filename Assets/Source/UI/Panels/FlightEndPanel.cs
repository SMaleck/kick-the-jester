using Assets.Source.App;
using Assets.Source.Behaviours.GameLogic;
using Assets.Source.UI.Elements;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class FlightEndPanel : AbstractPanel
    {               
        [SerializeField] Button retryButton;
        [SerializeField] Button shopButton;

        [SerializeField] Text distance;
        [SerializeField] Text bestDistance;
        [SerializeField] GameObject newBestLabel;

        [SerializeField] RectTransform currencyContainer;
        [SerializeField] GameObject pfCurrencyItem;
        [SerializeField] Text currency;

        
        private int finalCurrencyCollected;
        private int initialCurrencyAmount;
        private int finalCurrencyAmount;

        public override void Setup()
        {
            base.Setup();
            newBestLabel.SetActive(false);

            App.Cache.GameLogic.StateProperty
                               .Where(e => e.Equals(GameState.End))
                               .Subscribe(_ => OnFlightEnd())
                               .AddTo(this);

            retryButton.OnClickAsObservable().Subscribe(_ => OnRetryClicked()).AddTo(this);
            shopButton.OnClickAsObservable().Subscribe(_ => OnShopClicked()).AddTo(this);
            
            App.Cache.Jester.DistanceProperty
                            .Subscribe(x => { distance.text = x.ToMeters() + "m"; })
                            .AddTo(this);

            Kernel.PlayerProfileService.RP_BestDistance
                                       .SubscribeToText(bestDistance, e => string.Format("{0}m", e.ToMeters()))
                                       .AddTo(this);

            initialCurrencyAmount = Kernel.PlayerProfileService.Currency;
            


            currency.text = initialCurrencyAmount + "G";
        }

        private void OnFlightEnd()
        {
            IDictionary<string, int> currencyResults = App.Cache.GameLogic.currencyRecorder.GetResults();
            List<CurrencyItem> currencyItems = new List<CurrencyItem>();

            // Create currency Items
            foreach(string key in currencyResults.Keys)
            {
                GameObject go = GameObject.Instantiate(pfCurrencyItem);
                go.GetComponent<RectTransform>().SetParent(currencyContainer);

                currencyItems.Add(go.GetComponent<CurrencyItem>());

                var ci = currencyItems.Last();
                ci.Label = key;
                ci.Value = currencyResults[key].ToString();
            }

            // Adjust positions for all items
            for(int i = 0; i < currencyItems.Count; i++)
            {
                RectTransform rectT = currencyItems[i].gameObject.GetComponent<RectTransform>();
                Vector3 newPos = new Vector3(rectT.position.x, currencyItems[i].Height * i, rectT.position.z);
                rectT.position = newPos;
            }

            /*
            int currencyEarnedByDistance = App.Cache.GameLogic.currencyRecorder.CalculateCurrencyEarnedInFlight();
            finalCurrencyCollected = App.Cache.GameLogic.currencyRecorder.CurrencyCollected;
            finalCurrencyAmount = Kernel.PlayerProfileService.Currency;

            LeanTween.sequence()
                .append(LeanTween.value(gameObject, OnCurrencyCollectedTween, 0f, finalCurrencyCollected, 1f))
                .append(LeanTween.value(gameObject, OnCurrencyDistanceTween, 0f, currencyEarnedByDistance, 1f))
                .append(LeanTween.value(gameObject, OnCurrencyTotalTween, initialCurrencyAmount, finalCurrencyAmount, 1f));

            */
            gameObject.SetActive(true);
        }

        private void OnCurrencyTotalTween(float obj)
        {
            currency.text = Mathf.RoundToInt(obj) + "G";
        }

        private void OnCurrencyDistanceTween(float currency)
        {
            //currencyEarnedByDistance.text = Mathf.RoundToInt(currency) + "G";
        }

        private void OnCurrencyCollectedTween(float currency)
        {
            //currencyCollected.text = Mathf.RoundToInt(currency) + "G";
        }

        private void OnRetryClicked()
        {
            Kernel.SceneTransitionService.ToGame();
        }


        private void OnShopClicked()
        {
            Kernel.SceneTransitionService.ToShop();
        }
    }
}
