using Assets.Source.Entities.GenericComponents;
using Assets.Source.Services.Audio;
using UniRx;

namespace Assets.Source.Entities.Jester.Components
{
    public class SoundEffect : AbstractPausableComponent<JesterEntity>
    {
        private readonly AudioService _audioService;

        private bool _listenForImpacts = false;

        public SoundEffect(JesterEntity owner, AudioService audioService)
            : base(owner)
        {
            _audioService = audioService;

            Owner.OnKicked
                .Subscribe(_ => _listenForImpacts = true)
                .AddTo(owner);

            owner.Collisions.OnGround
                .Where(_ => _listenForImpacts)
                .Subscribe(_ => OnGroundImpact())
                .AddTo(owner);
        }
        
        private void OnGroundImpact()
        {
            _audioService.PlayEffectRandomized(AudioClipType.Sfx_Impact);
        }
    }
}
