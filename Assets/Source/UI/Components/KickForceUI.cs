using UnityEngine.UI;

namespace Assets.Source.UI.Components
{
    public class KickForceUI : BaseComponent
    {
        Slider slider;

        void Start()
        {            
            slider = gameObject.GetComponentInChildren<Slider>();
            RegisterWithManager();
        }

        public void UpdateUI(float value, float max)
        {
            slider.maxValue = max;            
            slider.value = value;
        }
    }
}