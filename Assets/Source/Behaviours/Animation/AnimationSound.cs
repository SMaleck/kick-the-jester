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
                App.Cache.Kernel.AudioService.PlayRandomizedSFX(clip);
            }
            else
            {
                App.Cache.Kernel.AudioService.PlaySFX(clip);
            }            
        }
    }
}
