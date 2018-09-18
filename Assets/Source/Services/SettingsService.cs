using Assets.Source.App.Configuration;
using Assets.Source.Services.Savegame;
using UniRx;

namespace Assets.Source.Services
{
    public class SettingsService
    {
        // TODO Persist settings across sessions
        // TODO Add mechanism to mute/unmute volumes easily

        private readonly SavegameService _savegameService;
        private readonly DefaultSettingsConfig _defaultSettingsConfig;

        public readonly BoolReactiveProperty IsMusicMuted = new BoolReactiveProperty();       
        public readonly FloatReactiveProperty MusicVolume = new FloatReactiveProperty();

        public readonly BoolReactiveProperty IsEffectsMuted = new BoolReactiveProperty();
        public readonly FloatReactiveProperty EffectsVolume = new FloatReactiveProperty();


        public SettingsService(SavegameService savegameService, DefaultSettingsConfig defaultSettingsConfig)
        {
            _savegameService = savegameService;
            _defaultSettingsConfig = defaultSettingsConfig;

            RestoreDefaults();
        }


        public void RestoreDefaults()
        {
            IsMusicMuted.Value = false;
            MusicVolume.Value = _defaultSettingsConfig.MusicVolume;

            IsEffectsMuted.Value = false;
            EffectsVolume.Value = _defaultSettingsConfig.EffectVolume;
        }
    }
}
