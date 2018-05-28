using Assets.Source.App;
using UnityEngine;

namespace Assets.Source.Config
{
    public class JesterMovementConfig
    {
        public Vector3 Direction = new Vector3(1, 1, 0);
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
