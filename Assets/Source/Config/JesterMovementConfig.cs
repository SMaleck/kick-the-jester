using Assets.Source.App;

namespace Assets.Source.Config
{
    public class JesterMovementConfig
    {
        public float MaxVelocity;
        public float KickForce;
        public float ShootForce;
        public int ShootCount;
        
    
        public JesterMovementConfig()
        {
            MaxVelocity = Kernel.PlayerProfileService.MaxVelocity;
            KickForce = Kernel.PlayerProfileService.KickForce;
            ShootForce = Kernel.PlayerProfileService.ShootForce;
            ShootCount = Kernel.PlayerProfileService.ShootCount;            
        }
    }
}
