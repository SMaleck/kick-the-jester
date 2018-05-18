using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class KickForcePanel : MonoBehaviour
    {
        [SerializeField] private UIProgressBar kickForceSlider;

        private void Start()
        {                                    
            App.Cache.jester.RelativeKickForceProperty                                                
                            .Subscribe((float value) => { kickForceSlider.fillAmount = value; })
                            .AddTo(this);

            App.Cache.jester.IsStartedProperty.Where(e => e).Subscribe(_ => { gameObject.SetActive(false); });
        }
    }
}