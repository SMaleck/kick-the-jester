using Assets.Source.Entities.GenericComponents;
using Assets.Source.Entities.Jester.Config;
using Assets.Source.Services.Audio;
using UniRx;

namespace Assets.Source.Entities.Jester.Components
{
    public class SoundEffect : AbstractPausableComponent<JesterEntity>
    {
        private readonly JesterSoundEffectsConfig _config;
        private readonly AudioService _audioService;

        private bool _listenForImpacts = false;

        public SoundEffect(JesterEntity owner, JesterSoundEffectsConfig config, AudioService audioService)
            : base(owner)
        {
            _config = config;
            _audioService = audioService;

            Owner.OnKicked
                .Subscribe(_ => _listenForImpacts= true)
                .AddTo(owner);

            owner.Collisions.OnGround
                .Where(_ => _listenForImpacts)
                .Subscribe(_ => OnGroundImpact())
                .AddTo(owner);
        }


        private void OnGroundImpact()
        {
            _audioService.PlayEffectRandomized(_config.GroundHit);
        }
    }
}
