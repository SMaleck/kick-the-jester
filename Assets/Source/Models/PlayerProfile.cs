using Assets.Source.App;
using System;

namespace Assets.Source.Models
{
    public class PlayerProfile : Serializable
    {
        public const float BASE_KICK_FORCE = 600f;
        public const int BASE_KICK_COUNT = 2;

        public float KickForce;
        public Int32 KickCount;
        public Int32 BestDistance;
        public Int32 Currency;

        public PlayerProfile() {
            KickForce = BASE_KICK_FORCE;
            KickCount = BASE_KICK_COUNT;
            BestDistance = 0;
            Currency = 0;
        }
    }
}
