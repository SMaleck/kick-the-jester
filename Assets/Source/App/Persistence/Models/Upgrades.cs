using Newtonsoft.Json;
using UniRx;

namespace Assets.Source.App.Persistence.Models
{
    public class Upgrades : AbstractPersistentModel
    {
        [JsonIgnore]
        public IntReactiveProperty RP_MaxVelocityLevel = new IntReactiveProperty(0);
        public int MaxVelocityLevel
        {
            get { return RP_MaxVelocityLevel.Value; }
            set { RP_MaxVelocityLevel.Value = value; }
        }

        [JsonIgnore]
        public IntReactiveProperty RP_KickForceLevel = new IntReactiveProperty(0);
        public int KickForceLevel
        {
            get { return RP_KickForceLevel.Value; }
            set { RP_KickForceLevel.Value = value; }
        }

        [JsonIgnore]
        public IntReactiveProperty RP_ShootForceLevel = new IntReactiveProperty(0);
        public int ShootForceLevel
        {
            get { return RP_ShootForceLevel.Value; }
            set { RP_ShootForceLevel.Value = value; }
        }

        [JsonIgnore]
        public IntReactiveProperty RP_ShootCountLevel = new IntReactiveProperty(0);
        public int ShootCountLevel
        {
            get { return RP_ShootCountLevel.Value; }
            set { RP_ShootCountLevel.Value = value; }
        }


        /* ---------------------------------------------------------------------- */
        public Upgrades() : base()
        { }
    }
}
