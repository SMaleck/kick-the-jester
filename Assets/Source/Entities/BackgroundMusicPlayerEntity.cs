using Assets.Source.Services.Audio;
using UnityEngine;
using Zenject;

namespace Assets.Source.Entities
{
    public class BackgroundMusicPlayerEntity : AbstractMonoEntity
    {
        [SerializeField] AudioClip _backgroundMusic;

        private AudioService _audioService;


        public override void Initialize()
        {
            _audioService.PlayMusic(_backgroundMusic);
        }


        [Inject]
        private void Inject(AudioService audioService)
        {            
            _audioService = audioService;            
        }
    }
}
