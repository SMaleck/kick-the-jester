using Assets.Source.App.Audio;
using Assets.Source.Entities.Jester.Config;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class SoundEffect : AbstractComponent<JesterContainer>
    {
        private readonly JesterSoundEffectsConfig config;
        private readonly AudioService audioService;

        public SoundEffect(JesterContainer owner, JesterSoundEffectsConfig config, AudioService audioService)
            : base(owner, false)
        {
            this.config = config;
            this.audioService = audioService;

            owner.Collisions.OnGround(() => audioService.PlayRandomizedSFX(config.GroundHit));
        }
    }
}
