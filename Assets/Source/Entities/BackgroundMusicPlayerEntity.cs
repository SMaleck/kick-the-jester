using Assets.Source.Services.Audio;
using UnityEngine;

// ToDo Put in respective scenes
namespace Assets.Source.Entities
{
    public class BackgroundMusicPlayerEntity : AbstractMonoEntity
    {
        [SerializeField] AudioClip _backgroundMusic;

        private AudioService _audioService;


        public override void Initialize() { }

        private void Inject(AudioService audioService)
        {
            _audioService = audioService;
            audioService.PlayMusic(_backgroundMusic);
        }
    }
}
