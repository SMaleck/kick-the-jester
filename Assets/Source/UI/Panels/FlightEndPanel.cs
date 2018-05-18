using Assets.Source.App;
using Assets.Source.GameLogic;
using System;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class FlightEndPanel : MonoBehaviour
    {
        [SerializeField] Button retryButton;
        [SerializeField] Button shopButton;
        [SerializeField] Text distance;
        [SerializeField] Text bestDistance;
        [SerializeField] Text currencyCollected;
        [SerializeField] Text currencyEarnedByDistance;
        [SerializeField] Text currency;

        private int finalCurrencyCollected;
        private int initialCurrencyAmount;
        private int finalCurrencyAmount;

        private void Awake()
        {
            gameObject.SetActive(false);
            
            App.Cache.CurrencyRecorder.OnCommit(OnFlightEnd);

            retryButton.OnClickAsObservable().Subscribe(_ => OnRetryClicked()).AddTo(this);
            shopButton.OnClickAsObservable().Subscribe(_ => OnShopClicked()).AddTo(this);
            
            App.Cache.jester.DistanceProperty
                            .Subscribe(x => { distance.text = x.ToMeters() + "m"; })
                            .AddTo(this);

            Kernel.PlayerProfileService.bestDistanceProperty
                                       .Subscribe(x => { bestDistance.text = x.ToString() + "m"; })
                                       .AddTo(this);

            initialCurrencyAmount = Kernel.PlayerProfileService.Currency;
            //Kernel.PlayerProfileService.currencyProperty
            //                           .Subscribe(x => { finalCurrencyAmount = x; })
            //                           .AddTo(this);

            //App.Cache.CurrencyRecorder.CurrencyCollectedProperty
            //                          .Subscribe(x => { finalCurrencyCollected = x; })
            //                          .AddTo(this);
            

            currencyCollected.text = "0G";
            currencyEarnedByDistance.text = "0G";
            currency.text = initialCurrencyAmount + "G";
        }

        private void OnFlightEnd()
        {
            int currencyEarnedByDistance = App.Cache.CurrencyRecorder.CalculateCurrencyEarnedInFlight();
            finalCurrencyCollected = App.Cache.CurrencyRecorder.CurrencyCollected;
            finalCurrencyAmount = Kernel.PlayerProfileService.Currency;

            LeanTween.sequence()
                .append(LeanTween.value(gameObject, OnCurrencyCollectedTween, 0f, finalCurrencyCollected, 1.5f))
                .append(LeanTween.value(gameObject, OnCurrencyDistanceTween, 0f, currencyEarnedByDistance, 1.5f))
                .append(LeanTween.value(gameObject, OnCurrencyTotalTween, initialCurrencyAmount, finalCurrencyAmount, 1f));

            gameObject.SetActive(true);
        }

        private void OnCurrencyTotalTween(float obj)
        {
            currency.text = Mathf.RoundToInt(obj) + "G";
        }

        private void OnCurrencyDistanceTween(float currency)
        {
            currencyEarnedByDistance.text = Mathf.RoundToInt(currency) + "G";
        }

        private void OnCurrencyCollectedTween(float currency)
        {
            currencyCollected.text = Mathf.RoundToInt(currency) + "G";
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
