using Assets.Source.AppKernel;
using UnityEngine;

namespace Assets.Source.Behaviours
{
    public class MusicPlayer : AbstractBehaviour
    {
        [SerializeField] AudioClip musicClip;

        private void Start()
        {
            Kernel.AudioService.PlayLoopingBGM(musicClip);
        }
    }
}
