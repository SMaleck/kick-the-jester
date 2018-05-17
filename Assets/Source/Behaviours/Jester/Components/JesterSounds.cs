using Assets.Source.AppKernel;
using Assets.Source.Config;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class JesterSounds
    {
        private readonly JesterSoundEffectsConfig config;

        public JesterSounds(Jester owner, JesterSoundEffectsConfig config)
        {
            this.config = config;

            owner.Collisions.OnGround(() => Kernel.AudioService.PlayRandomizedSFX(config.GroundHit));
        }
    }
}
