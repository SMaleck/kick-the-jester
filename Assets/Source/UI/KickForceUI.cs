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

            // Register for Updates
            App.Cache.rxState.AttachForRelativeKickForce(this.UpdateUI);
            App.Cache.RepoRx.JesterStateRepository.OnStartedFlight(HideUI);
        }

        private void HideUI()
        {
            gameObject.SetActive(false);
        }

        private void UpdateUI(int value)
        {                        
            slider.value = value;
        }
    }
}