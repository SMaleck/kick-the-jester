using Newtonsoft.Json;
using UniRx;

namespace Assets.Source.Services.Savegames.StorageModels
{
    public class SettingsStorageModel : AbstractPersistentModel
    {
        [JsonIgnore]
        public BoolReactiveProperty IsMusicMuted = new BoolReactiveProperty();
        public bool IsMusicMutedData
        {
            get { return IsMusicMuted.Value; }
            set { IsMusicMuted.Value = value; }
        }
        
        [JsonIgnore]
        public BoolReactiveProperty IsEffectsMuted = new BoolReactiveProperty();
        public bool IsEffectsMutedData
        {
            get { return IsEffectsMuted.Value; }
            set { IsEffectsMuted.Value = value; }
        }
    }
}
