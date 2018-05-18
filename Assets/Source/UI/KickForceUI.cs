using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Components
{
    public class KickForceUI : MonoBehaviour
    {
        Slider slider;

        private void Start()
        {            
            slider = gameObject.GetComponentInChildren<Slider>();
            slider.maxValue = 100;                        

            App.Cache.RepoRx.GameStateRepository.RelativeKickForceProperty
                                                .TakeUntilDestroy(this)
                                                .Subscribe(UpdateUI);

            App.Cache.jester.OnStarted.Subscribe(_ => { gameObject.SetActive(false); });
        }

        private void UpdateUI(int value)
        {                        
            slider.value = value;
        }
    }
}