using Assets.Source.App;
using Assets.Source.GameLogic;
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
        [SerializeField] Text currency;

        private void Awake()
        {
            gameObject.SetActive(false);

            // Activate on Flight End
            App.Cache.GameStateMachine.StateProperty                                                
                                      .Where(e => e.Equals(GameState.End))
                                      .Subscribe(_ => OnFlightEnd())
                                      .AddTo(this);

            retryButton.OnClickAsObservable().Subscribe(_ => OnRetryClicked());
            shopButton.OnClickAsObservable().Subscribe(_ => OnShopClicked());

            App.Cache.jester.CollectedCurrencyProperty
                            .Subscribe(x => { currencyCollected.text = x.ToString() + "G"; })
                            .AddTo(this);
            
            Kernel.PlayerProfileService.bestDistanceProperty                                       
                                       .Subscribe(x => { bestDistance.text = x.ToString() + "m"; })
                                       .AddTo(this);
            
        }

        private void OnFlightEnd()
        {
            distance.text = App.Cache.jester.Distance.ToMeters().ToString() + "m";
            currency.text = Kernel.PlayerProfileService.Currency.ToString() + "G";

            gameObject.SetActive(true);
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
