using UnityEngine.UI;

namespace Assets.Source.UI.Components
{
    public class KickForceUI : BaseComponent
    {
        Slider slider;

        void Start()
        {            
            slider = gameObject.GetComponentInChildren<Slider>();
            slider.maxValue = 100;

            RegisterWithManager();
        }

        public void UpdateUI(int value)
        {                        
            slider.value = value;
        }
    }
}