using Assets.Source.App;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class FlightEndPanel : AbstractPanel
    {
        [SerializeField] GameObject newBestLabel;

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

        public override void Setup()
        {
            base.Setup();
            newBestLabel.SetActive(false);


            App.Cache.GameLogic.currencyRecorder.OnCommit(OnFlightEnd);

            retryButton.OnClickAsObservable().Subscribe(_ => OnRetryClicked()).AddTo(this);
            shopButton.OnClickAsObservable().Subscribe(_ => OnShopClicked()).AddTo(this);
            
            App.Cache.Jester.DistanceProperty
                            .Subscribe(x => { distance.text = x.ToMeters() + "m"; })
                            .AddTo(this);

            Kernel.PlayerProfileService.RP_BestDistance
                                       .SubscribeToText(bestDistance, e => string.Format("{0}m", e.ToMeters()))
                                       .AddTo(this);

            initialCurrencyAmount = Kernel.PlayerProfileService.Currency;
            

            currencyCollected.text = "0G";
            currencyEarnedByDistance.text = "0G";
            currency.text = initialCurrencyAmount + "G";
        }

        private void OnFlightEnd()
        {
            int currencyEarnedByDistance = App.Cache.GameLogic.currencyRecorder.CalculateCurrencyEarnedInFlight();
            finalCurrencyCollected = App.Cache.GameLogic.currencyRecorder.CurrencyCollected;
            finalCurrencyAmount = Kernel.PlayerProfileService.Currency;

            LeanTween.sequence()
                .append(LeanTween.value(gameObject, OnCurrencyCollectedTween, 0f, finalCurrencyCollected, 1f))
                .append(LeanTween.value(gameObject, OnCurrencyDistanceTween, 0f, currencyEarnedByDistance, 1f))
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
