using Assets.Source.App.Configuration;
using Assets.Source.Mvc.Models;
using Assets.Source.Services.Audio;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Mvc.ServiceControllers
{
    public class AudioSettingsController : AbstractDisposable
    {
        private readonly SettingsModel _settingsModel;
        private readonly AudioService _audioService;
        private readonly DefaultSettingsConfig _defaultSettingsConfig;

        public AudioSettingsController(SettingsModel settingsModel, AudioService audioService, DefaultSettingsConfig defaultSettingsConfig)
        {
            _settingsModel = settingsModel;
            _audioService = audioService;
            _defaultSettingsConfig = defaultSettingsConfig;

            _settingsModel.IsMusicMuted
                .Subscribe(isMuted => 
                {
                    var volume = isMuted ? 0 : defaultSettingsConfig.MusicVolume;
                    _audioService.SetMusicVolume(volume);
                })
                .AddTo(Disposer);

            _settingsModel.IsSoundMuted
                .Subscribe(isMuted =>
                {
                    var volume = isMuted ? 0 : defaultSettingsConfig.EffectVolume;
                    _audioService.SetEffectsVolume(volume);
                })
                .AddTo(Disposer);
        }
    }
}
