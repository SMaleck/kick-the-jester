using Assets.Source.Services.Savegame;
using UniRx;

namespace Assets.Source.Mvc.Models
{
    public class SettingsModel
    {
        public BoolReactiveProperty IsMusicMuted;
        public BoolReactiveProperty IsEffectsMuted;

        // ToDo Implement restore
        public void RestoreDefaults() { }

        public SettingsModel(SavegameService savegameService)
        {
            var settings = savegameService.Settings;

            IsMusicMuted = settings.IsMusicMuted;
            IsEffectsMuted = settings.IsEffectsMuted;
        }
    }
}
