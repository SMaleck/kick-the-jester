using System;

namespace Assets.Source.App.Storage
{
    public class PlayerProfile : Serializable
    {
        public const float BASE_MAX_VELOCITY = 5f;
        public const float BASE_KICK_FORCE = 600f;
        public const int BASE_KICK_COUNT = 2;

        public float MaxVelocity = BASE_MAX_VELOCITY;
        public float KickForce = BASE_KICK_FORCE;
        public Int32 KickCount = BASE_KICK_COUNT;
        public Int32 BestDistance = 0;
        public Int32 Currency = 0;

        public PlayerProfile() { }
    }
}
