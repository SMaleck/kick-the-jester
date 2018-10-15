using Assets.Source.Mvc.ServiceControllers;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Util.UI
{
    // ToDo [IMPORTANT] Use this derived button for sound effects
    public class RichButton : Button
    {
        [SerializeField] private ButtonAudioEvent _audioEventType = ButtonAudioEvent.Default;

        protected override void Awake()
        {
            base.Awake();

            this.OnClickAsObservable()
                .Where(_ => !_audioEventType.Equals(ButtonAudioEvent.None))
                .Subscribe(_ => { MessageBroker.Default.Publish(_audioEventType); })
                .AddTo(this);
        }
    }
}
