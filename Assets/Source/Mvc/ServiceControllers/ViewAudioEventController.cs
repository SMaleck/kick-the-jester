using Assets.Source.Services.Audio;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Mvc.ServiceControllers
{
    public class ViewAudioEventController : AbstractDisposable
    {
        private readonly AudioService _audioService;

        public ViewAudioEventController(AudioService audioService)
        {
            _audioService = audioService;

            MessageBroker.Default.Receive<ViewAudioEvent>()
                .Subscribe(ResolveViewAudioEvent)
                .AddTo(Disposer);

            MessageBroker.Default.Receive<ButtonAudioEvent>()
                .Subscribe(ResolveViewAudioEvent)
                .AddTo(Disposer);
        }

        private void ResolveViewAudioEvent(ViewAudioEvent eventType)
        {
            _audioService.PlayUiEffect(eventType.ToAudioClipType());
        }

        private void ResolveViewAudioEvent(ButtonAudioEvent eventType)
        {
            _audioService.PlayUiEffect(eventType.ToAudioClipType());
        }
    }
}
