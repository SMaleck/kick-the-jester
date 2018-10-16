using Assets.Source.Entities.Jester;
using Assets.Source.Mvc.Models;
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
        private UserInputModel _userInputModel;

        private enum AnimState { Idle, Kick };
        private bool _hasKicked = false;


        [Inject]
        private void Inject(JesterEntity jesterEntity, AudioService audioService, ParticleService particleService, UserInputModel userInputModel)
        {
            _jesterEntity = jesterEntity;
            _audioService = audioService;
            _particleService = particleService;
            _userInputModel = userInputModel;
        }

        public override void Initialize()
        {
            _userInputModel.OnClickedAnywhere
                .Where(_ => !IsPaused.Value)
                .Subscribe(_ => OnKick())
                .AddTo(this);
        }

        private void OnKick()
        {
            if (_hasKicked)
            {
                return;
            }

            _animator.Play(AnimState.Kick.ToString());
            _hasKicked = true;
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
