using Assets.Source.Services.Savegame.Models;
using Newtonsoft.Json;
using UniRx;

namespace Assets.Source.Services.Savegame.StorageModels
{
    public class UpgradesStorageModel : AbstractPersistentModel
    {
        [JsonIgnore]
        public IntReactiveProperty MaxVelocityLevel = new IntReactiveProperty(0);
        public int MaxVelocityLevelData
        {
            get { return MaxVelocityLevel.Value; }
            set { MaxVelocityLevel.Value = value; }
        }

        [JsonIgnore]
        public IntReactiveProperty KickForceLevel = new IntReactiveProperty(0);
        public int KickForceLevelData
        {
            get { return KickForceLevel.Value; }
            set { KickForceLevel.Value = value; }
        }

        [JsonIgnore]
        public IntReactiveProperty ShootForceLevel = new IntReactiveProperty(0);
        public int ShootForceLevelData
        {
            get { return ShootForceLevel.Value; }
            set { ShootForceLevel.Value = value; }
        }

        [JsonIgnore]
        public IntReactiveProperty ShootCountLevel = new IntReactiveProperty(0);
        public int ShootCountLevelData
        {
            get { return ShootCountLevel.Value; }
            set { ShootCountLevel.Value = value; }
        }        
    }
}
