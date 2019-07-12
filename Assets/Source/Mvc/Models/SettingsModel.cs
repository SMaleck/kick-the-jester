using Assets.Source.Services.Savegame;
using Assets.Source.Services.Savegame.StorageModels;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Mvc.Models
{
    public class SettingsModel : AbstractDisposable
    {
        private readonly SettingsStorageModel _settingsStorageModel;

        public IReadOnlyReactiveProperty<bool> IsMusicMuted => _settingsStorageModel.IsMusicMuted;
        public IReadOnlyReactiveProperty<bool> IsEffectsMuted => _settingsStorageModel.IsEffectsMuted;

        public SettingsModel(SavegameService savegameService)
        {
            _settingsStorageModel = savegameService.Settings;
        }

        public void SetIsMusicMuted(bool value)
        {
            _settingsStorageModel.IsMusicMuted.Value = value;
        }

        public void SetIsEffectsMuted(bool value)
        {
            _settingsStorageModel.IsEffectsMuted.Value = value;
        }

        public void RestoreDefaults()
        {
            SetIsMusicMuted(false);
            SetIsEffectsMuted(false);
        }
    }
}
