using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Util.UI
{
    public enum ButtonPressedEvent
    {
        Default
    }


    // TODO Fix Inspector with https://answers.unity.com/questions/1304097/subclassing-button-public-variable-wont-show-up-in.html
    public class XButton : Button
    {
        [SerializeField] ButtonPressedEvent Type = ButtonPressedEvent.Default;

        protected override void Awake()
        {
            base.Awake();

            this.OnClickAsObservable()
                .Subscribe(_ => { MessageBroker.Default.Publish(Type); })
                .AddTo(this);
        }
    }
}
