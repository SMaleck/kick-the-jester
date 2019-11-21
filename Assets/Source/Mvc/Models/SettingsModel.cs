using Assets.Source.Services.Savegames;
using Assets.Source.Services.Savegames.Models;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Mvc.Models
{
    public class SettingsModel : AbstractDisposable
    {
        private readonly SettingsSavegame _settingsSavegame;

        public IReadOnlyReactiveProperty<bool> IsMusicMuted => _settingsSavegame.IsMusicMuted;
        public IReadOnlyReactiveProperty<bool> IsSoundMuted => _settingsSavegame.IsSoundMuted;

        public SettingsModel(ISavegameService savegameService)
        {
            _settingsSavegame = savegameService.Savegame.SettingsSavegame;
        }

        public void SetIsMusicMuted(bool value)
        {
            _settingsSavegame.IsMusicMuted.Value = value;
        }

        public void SetIsEffectsMuted(bool value)
        {
            _settingsSavegame.IsSoundMuted.Value = value;
        }

        public void RestoreDefaults()
        {
            SetIsMusicMuted(false);
            SetIsEffectsMuted(false);
        }
    }
}
