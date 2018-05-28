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
            MaxVelocity = Kernel.PlayerProfileService.MaxVelocity;
            Count = Kernel.PlayerProfileService.ShootCount;
        }
    }
}
