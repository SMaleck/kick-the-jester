using Assets.Source.App;

namespace Assets.Source.Config
{
    public class MotionShootConfig : MotionConfig
    {
        public float MaxVelocity;
        public int Count;

        public MotionShootConfig()
            : base()
        {
            MaxVelocity = App.Cache.Kernel.PlayerProfile.MaxVelocity;
            Count = App.Cache.Kernel.PlayerProfile.ShootCount;
        }
    }
}
