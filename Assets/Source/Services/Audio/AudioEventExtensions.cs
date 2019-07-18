using System;

namespace Assets.Source.Services.Audio
{
    public static class AudioEventExtensions
    {
        public static bool IsNone(this AudioClipType audioClipType)
        {
            return audioClipType == AudioClipType.None;
        }

        public static AudioClipType ToAudioClipType(this ViewAudioEvent viewAudioEvent)
        {
            switch (viewAudioEvent)
            {
                case ViewAudioEvent.PanelSlideOpen:
                    return AudioClipType.Ui_PanelSlideOpen;

                case ViewAudioEvent.PanelSlideClose:
                    return AudioClipType.Ui_PanelSlideClose;

                default:
                    throw new ArgumentOutOfRangeException(nameof(viewAudioEvent), viewAudioEvent, null);
            }
        }

        public static AudioClipType ToAudioClipType(this ButtonAudioEvent buttonAudioEvent)
        {
            switch (buttonAudioEvent)
            {
                case ButtonAudioEvent.None:
                case ButtonAudioEvent.Default:
                    return AudioClipType.None;

                case ButtonAudioEvent.Upgrade:
                    return AudioClipType.Ui_Sparkles;

                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonAudioEvent), buttonAudioEvent, null);
            }
        }
    }
}
