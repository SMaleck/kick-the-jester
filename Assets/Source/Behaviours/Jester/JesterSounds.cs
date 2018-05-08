using UnityEngine;

namespace Assets.Source.Behaviours.Jester
{
    public class JesterSounds : AbstractBehaviour
    {
        [SerializeField] private AudioClip acGroundHit;        

        private void Awake()
        {
            App.Cache.jester.Collisions.OnGround(() => App.Cache.audioService.PlayRandomizedSFX(acGroundHit));            
        }
    }
}
