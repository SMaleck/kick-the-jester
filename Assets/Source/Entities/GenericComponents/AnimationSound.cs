using Assets.Source.Services.Audio;
using UnityEngine;
using Zenject;

namespace Assets.Source.Entities.GenericComponents
{    
    public class AnimationSound : MonoBehaviour
    {
        [SerializeField] private bool randomize;
        [SerializeField] private AudioClipType _audioClipType;

        private AudioService _audioService;

        public void PlaySound()
        {
            if (randomize)
            {
                _audioService.PlayEffectRandomized(_audioClipType);
            }
            else
            {
                _audioService.PlayEffect(_audioClipType);
            }            
        }


        [Inject]
        private void Inject(AudioService audioService)
        {
            _audioService = audioService;
        }
    }
}
