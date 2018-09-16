using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Util.UI
{
    public enum ButtonPressedSoundEvent
    {
        None,
        Default
    }

    
    public class ButtonSound : MonoBehaviour
    {
        [SerializeField] private ButtonPressedSoundEvent _soundEffect = ButtonPressedSoundEvent.Default;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            
            _button?.OnClickAsObservable()
                .Where(_ => !_soundEffect.Equals(ButtonPressedSoundEvent.None))
                .Subscribe(_ => { MessageBroker.Default.Publish(_soundEffect); })
                .AddTo(this);
        }
    }
}
