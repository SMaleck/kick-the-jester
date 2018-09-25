using Assets.Source.Entities.Jester;
using Assets.Source.Services;
using Assets.Source.Services.Audio;
using Assets.Source.Services.Particles;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Source.Entities.Knight
{
    public class KnightEntity : AbstractPausableMonoEntity
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioClip _kickSound;
        [SerializeField] private GameObject _pfxKickSwoosh;
        [SerializeField] private Transform _pfxSlotKickSwoosh;

        private JesterEntity _jesterEntity;
        private AudioService _audioService;
        private ParticleService _particleService;
        private UserControlService _userControlService;
        
        private enum AnimState { Idle, Kick };
        private bool hasKicked = false;


        [Inject]
        private void Inject(JesterEntity jesterEntity, AudioService audioService, ParticleService particleService, UserControlService userControlService)
        {
            _jesterEntity = jesterEntity;
            _audioService = audioService;
            _particleService = particleService;
            _userControlService = userControlService;
        }

        public override void Initialize()
        {
            _userControlService.OnKick
                .Where(_ => !IsPaused.Value)
                .Subscribe(_ => OnKick())
                .AddTo(this);
        }

        private void OnKick()
        {
            if (hasKicked)
            {
                return;
            }

            _animator.Play(AnimState.Kick.ToString());
            hasKicked = true;
        }

        public void PlayKickSwooshEffect()
        {
            _particleService.PlayAt(_pfxKickSwoosh, _pfxSlotKickSwoosh.position);
        }

        public void OnKickAnimationEnd()
        {            
            _audioService.PlayEffectRandomized(_kickSound);
            _jesterEntity.OnKicked.Execute();
        }
    }
}
