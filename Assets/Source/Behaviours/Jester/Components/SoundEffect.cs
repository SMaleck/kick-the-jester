using Assets.Source.AppKernel;
using Assets.Source.Config;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class SoundEffect : AbstractComponent<Jester>
    {
        private readonly JesterSoundEffectsConfig config;

        public SoundEffect(Jester owner, JesterSoundEffectsConfig config)
            : base(owner, false)
        {
            this.config = config;

            owner.Collisions.OnGround(() => Kernel.AudioService.PlayRandomizedSFX(config.GroundHit));
        }
    }
}
