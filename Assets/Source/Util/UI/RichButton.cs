using Assets.Source.Mvc.ServiceControllers;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Util.UI
{    
    public class RichButton : Button
    {
        [SerializeField] public ButtonAudioEvent AudioEventType = ButtonAudioEvent.Default;

        protected override void Awake()
        {
            base.Awake();

            this.OnClickAsObservable()
                .Where(_ => !AudioEventType.Equals(ButtonAudioEvent.None))
                .Subscribe(_ => { MessageBroker.Default.Publish(AudioEventType); })
                .AddTo(this);
        }
    }
}
