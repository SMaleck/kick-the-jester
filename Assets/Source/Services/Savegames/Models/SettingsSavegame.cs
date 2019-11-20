using UniRx;

namespace Assets.Source.Services.Savegames.Models
{
    public class SettingsSavegameData : AbstractSavegameData
    {
        public bool IsMusicMuted;
        public bool IsSoundMuted;
    }

    public class SettingsSavegame : AbstractSavegame
    {
        public readonly ReactiveProperty<bool> IsMusicMuted;
        public readonly ReactiveProperty<bool> IsSoundMuted;

        public SettingsSavegame(SettingsSavegameData data)
        {
            IsSoundMuted = CreateBoundProperty(
                data.IsSoundMuted,
                value => { data.IsSoundMuted = value; },
                Disposer);

            IsMusicMuted = CreateBoundProperty(
                data.IsMusicMuted,
                value => { data.IsMusicMuted = value; },
                Disposer);
        }
    }
}
