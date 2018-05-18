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

            App.Cache.jester.RelativeKickForceProperty                                                
                            .Subscribe(UpdateUI)
                            .AddTo(this);

            App.Cache.jester.IsStartedProperty.Where(e => e).Subscribe(_ => { gameObject.SetActive(false); });
        }

        private void UpdateUI(int value)
        {                        
            slider.value = value;
        }
    }
}