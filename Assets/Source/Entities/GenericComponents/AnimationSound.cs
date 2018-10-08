using Assets.Source.Services.Audio;
using UnityEngine;
using Zenject;

namespace Assets.Source.Entities.GenericComponents
{    
    public class AnimationSound : MonoBehaviour
    {
        [SerializeField] private bool randomize;
        [SerializeField] private AudioClip clip;

        private AudioService _audioService;

        public void PlaySound()
        {
            if (randomize)
            {
                _audioService.PlayEffectRandomized(clip);
            }
            else
            {
                _audioService.PlayEffect(clip);
            }            
        }


        [Inject]
        private void Inject(AudioService audioService)
        {
            _audioService = audioService;
        }
    }
}
