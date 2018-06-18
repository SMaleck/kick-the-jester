using Assets.Source.App;
using UnityEngine;

namespace Assets.Source.Config
{
    public class MotionConfig
    {
        public Vector3 Direction;
        public float Force;        
        
        public MotionConfig()
        {
            Direction = new Vector3(1, 1, 0);
            Force = Kernel.PlayerProfile.KickForce;
        }
    }
}
