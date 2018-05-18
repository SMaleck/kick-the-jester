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

            retryButton.OnClickAsObservable().Subscribe(_ => OnRetryClicked()).AddTo(this);
            shopButton.OnClickAsObservable().Subscribe(_ => OnShopClicked()).AddTo(this);

            App.Cache.jester.DistanceProperty
                            .TakeUntilDestroy(this)
                            .Subscribe(x => { distance.text = x.ToMeters() + "m"; });

            Kernel.PlayerProfileService.bestDistanceProperty
                                       .TakeUntilDestroy(this)
                                       .Subscribe(x => { bestDistance.text = x.ToString() + "m"; });
            Kernel.PlayerProfileService.currencyProperty
                                       .TakeUntilDestroy(this)
                                       .Subscribe(x => { currency.text = x + "G"; });

            App.Cache.CurrencyRecorder.CurrencyCollectedProperty
                                      .TakeUntilDestroy(this)
                                      .Subscribe(x => { currencyCollected.text = x + "G"; });
        }

        private void OnFlightEnd()
        {
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
