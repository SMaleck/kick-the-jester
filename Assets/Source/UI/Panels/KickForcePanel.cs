using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class KickForcePanel : AbstractPanel
    {
        [SerializeField] private UIProgressBar kickForceSlider;

        public override void Setup()
        {
            base.Setup();

            App.Cache.Jester.RelativeKickForceProperty                                                
                            .Subscribe((float value) => { kickForceSlider.fillAmount = value; })
                            .AddTo(this);

            App.Cache.Jester.IsStartedProperty.Where(e => e).Subscribe(_ => { gameObject.SetActive(false); });
        }
    }
}