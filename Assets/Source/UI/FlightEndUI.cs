using Assets.Source.Repositories;
using UniRx;
using UnityEngine;

namespace Assets.Source.UI
{
    public class FlightEndUI : MonoBehaviour
    {
        public GameObject Panel;
        void Start()
        {
            Panel.SetActive(false);

            // Register for Updates
            App.Cache.RepoRx.GameStateRepository.StateProperty
                                                .TakeUntilDestroy(this)
                                                .Where(e => e.Equals(GameState.End))
                                                .Subscribe(ActivatePanel);
        }

        private void ActivatePanel(GameState state)
        {
            Panel.SetActive(true);
        }
    }
}
