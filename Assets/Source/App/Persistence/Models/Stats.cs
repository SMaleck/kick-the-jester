using Newtonsoft.Json;
using UniRx;

namespace Assets.Source.App.Persistence.Models
{
    public class Stats : AbstractPersistentModel
    {
        [JsonIgnore]
        public BoolReactiveProperty RP_IsFirstStart = new BoolReactiveProperty(true);
        public bool IsFirstStart
        {
            get { return RP_IsFirstStart.Value; }
            set { RP_IsFirstStart.Value = value; }
        }

        [JsonIgnore]
        public IntReactiveProperty RP_BestDistance = new IntReactiveProperty(0);
        public int BestDistance
        {
            get { return RP_BestDistance.Value; }
            set { RP_BestDistance.Value = value; }
        }

        [JsonIgnore]
        public IntReactiveProperty RP_Currency = new IntReactiveProperty(0);
        public int Currency
        {
            get { return RP_Currency.Value; }
            set { RP_Currency.Value = value; }
        }


        /* ---------------------------------------------------------------------- */
        public Stats() : base()
        { }
    }
}
