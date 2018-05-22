using Assets.Source.App;
using UnityEngine;

namespace Assets.Source.Behaviours.Animation
{
    public class AnimationSound : MonoBehaviour
    {
        [SerializeField] private bool randomize;
        [SerializeField] private AudioClip clip;

        public void PlaySound()
        {
            if (randomize)
            {
                Kernel.AudioService.PlayRandomizedSFX(clip);
            }
            else
            {
                Kernel.AudioService.PlaySFX(clip);
            }            
        }
    }
}
