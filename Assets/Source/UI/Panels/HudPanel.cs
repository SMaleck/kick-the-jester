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
        [SerializeField] Text CollectedCurrencyText;

        private void Start()
        {
            PauseButton.OnClickAsObservable()
                       .Subscribe(_ => App.Cache.userControl.TooglePause())
                       .AddTo(this);

            // Flight Stats
            App.Cache.jester.DistanceProperty
                            .SubscribeToText(DistanceText, e => string.Format("{0}m", e.ToMeters()))
                            .AddTo(this);

            App.Cache.jester.HeightProperty
                            .SubscribeToText(HeightText, e => string.Format("{0}m", e.ToMeters()))
                            .AddTo(this);

            // Currency
            App.Cache.CurrencyRecorder.CurrencyCollectedProperty
                            .SubscribeToText(CollectedCurrencyText, e => string.Format("{0}G", e))                            
                            .AddTo(this);
        }
    }
}
