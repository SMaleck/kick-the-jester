using Assets.Source.App;
using Assets.Source.AppKernel;
using Assets.Source.Repositories;
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
        [SerializeField] Text currency;

        private void Awake()
        {
            gameObject.SetActive(false);

            // Activate on Flight End
            App.Cache.RepoRx.GameStateRepository.StateProperty
                                                .TakeUntilDestroy(this)
                                                .Where(e => e.Equals(GameState.End))
                                                .Subscribe(_ => OnFlightEnd());

            retryButton.OnClickAsObservable().Subscribe(_ => OnRetryClicked());
            shopButton.OnClickAsObservable().Subscribe(_ => OnShopClicked());

            App.Cache.RepoRx.JesterStateRepository.CollectedCurrencyProperty
                                                  .TakeUntilDestroy(this)
                                                  .Subscribe(x => { currencyCollected.text = x.ToString() + "G"; });
            
            Kernel.PlayerProfileService.bestDistanceProperty
                                                 .TakeUntilDestroy(this)
                                                 .Subscribe(x => { bestDistance.text = x.ToString() + "m"; });
            
        }

        private void OnFlightEnd()
        {
            distance.text = App.Cache.RepoRx.JesterStateRepository.Distance.ToMeters().ToString() + "m";
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
