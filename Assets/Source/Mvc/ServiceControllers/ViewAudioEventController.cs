using Assets.Source.App.Configuration;
using Assets.Source.Services.Audio;
using Assets.Source.Util;
using System.Linq;
using UniRx;

namespace Assets.Source.Mvc.ServiceControllers
{
    public enum ViewAudioEvent
    {
        PanelSlideOpen,
        PanelSlideClose
    }

    public enum ButtonAudioEvent
    {
        None,
        Default,
        Upgrade        
    }

    public class ViewAudioEventController : AbstractDisposable
    {
        private readonly AudioService _audioService;
        private readonly AudioEventConfig _config;

        public ViewAudioEventController(AudioService audioService, AudioEventConfig config)
        {
            _audioService = audioService;
            _config = config;

            MessageBroker.Default.Receive<ViewAudioEvent>()
                .Subscribe(ResolveViewAudioEvent)
                .AddTo(Disposer);

            MessageBroker.Default.Receive<ButtonAudioEvent>()
                .Subscribe(ResolveViewAudioEvent)
                .AddTo(Disposer);
        }

        public void ResolveViewAudioEvent(ViewAudioEvent eventType)
        {
            var audioSetting = _config.ViewAudioEventSettings.FirstOrDefault(e => e.AudioEventType.Equals(eventType));

            if (audioSetting?.AudioClip == null)
            {
                return;
            }

            _audioService.PlayEffect(audioSetting.AudioClip);
        }

        public void ResolveViewAudioEvent(ButtonAudioEvent eventType)
        {
            var audioSetting = _config.ButtonAudioEventSettings.FirstOrDefault(e => e.AudioEventType.Equals(eventType));

            if (audioSetting?.AudioClip == null)
            {
                return;
            }

            _audioService.PlayEffect(audioSetting.AudioClip);
        }
    }
}
