using System;

namespace Assets.Source.App.Storage
{
    public class PlayerProfile : Serializable
    {
        public bool IsFirstStart = true;

        public Int32 MaxVelocityLevel = 0;
        public Int32 KickForceLevel = 0;
        public Int32 ShootForceLevel = 0;
        public Int32 ShootCountLevel = 0;

        public Int32 BestDistance = 0;
        public Int32 Currency = 0;

        public PlayerProfile()
        {

        }
    }
}
