using Assets.Source.Repositories;
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

        private void Awake()
        {
            gameObject.SetActive(false);

            // Activate on Flight End
            App.Cache.RepoRx.GameStateRepository.StateProperty
                                                .TakeUntilDestroy(this)
                                                .Where(e => e.Equals(GameState.End))
                                                .Subscribe(_ => gameObject.SetActive(true));

            retryButton.OnClickAsObservable().Subscribe(_ => OnRetryClicked());
            shopButton.OnClickAsObservable().Subscribe(_ => OnShopClicked());            
        }


        private void OnRetryClicked()
        {
            App.Cache.Services.SceneTransitionService.ToGame();
        }


        private void OnShopClicked()
        {
            App.Cache.Services.SceneTransitionService.ToShop();
        }
    }
}
