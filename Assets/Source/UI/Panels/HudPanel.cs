using Assets.Source.App;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class HudPanel : MonoBehaviour
    {
        [SerializeField] Button PauseButton;

        [SerializeField] Text DistanceText;
        [SerializeField] Text HeightText;
        [SerializeField] Text BestDistanceText;

        [SerializeField] UIProgressBar velocityBar;

        private void Start()
        {
            PauseButton.OnClickAsObservable()
                       .Subscribe(_ => App.Cache.userControl.TooglePause())
                       .AddTo(this);
            
            App.Cache.jester.DistanceProperty
                            .SubscribeToText(DistanceText, e => string.Format("{0}m", e.ToMeters()))
                            .AddTo(this);

            App.Cache.jester.HeightProperty
                            .SubscribeToText(HeightText, e => string.Format("{0}m", e.ToMeters()))
                            .AddTo(this);

            Kernel.PlayerProfileService.RP_BestDistance
                                       .SubscribeToText(BestDistanceText, e => string.Format("{0}m", e.ToMeters()))
                                       .AddTo(this);

            App.Cache.jester.RelativeVelocityProperty
                            .Subscribe((float value) => { velocityBar.fillAmount = value; })
                            .AddTo(this);

            // Activate Velocity display only after start
            velocityBar.gameObject.SetActive(false);
            App.Cache.jester.IsStartedProperty.Where(e => e).Subscribe(_ => { velocityBar.gameObject.SetActive(true); });
        }
    }
}
