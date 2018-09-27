using Newtonsoft.Json;
using UniRx;

namespace Assets.Source.Services.Savegame.StorageModels
{
    public class ProfileStorageModel : AbstractPersistentModel
    {
        [JsonIgnore]
        public BoolReactiveProperty IsFirstStart = new BoolReactiveProperty();
        public bool IsFirstStartData
        {
            get { return IsFirstStart.Value; }
            set { IsFirstStart.Value = value; }
        }

        [JsonIgnore]
        public IntReactiveProperty Currency = new IntReactiveProperty();
        public int CurrencyData
        {
            get { return Currency.Value; }
            set { Currency.Value = value; }
        }

        [JsonIgnore]
        public FloatReactiveProperty BestDistance = new FloatReactiveProperty();
        public float BestDistanceData
        {
            get { return BestDistance.Value; }
            set { BestDistance.Value = value; }
        }

        [JsonIgnore]
        public IntReactiveProperty RoundsPlayed = new IntReactiveProperty();
        public int RoundsPlayedData
        {
            get { return RoundsPlayed.Value; }
            set { RoundsPlayed.Value = value; }
        }       
    }
}
