using Assets.Source.App;
using Assets.Source.Behaviours.Jester;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours
{
    public class Knight : AbstractBehaviour
    {        
        [SerializeField] private AudioClip KickSound;
        [SerializeField] private GameObject pfxKickSwoosh;
        [SerializeField] private Transform pfxSlotKickSwoosh;

        private bool hasKicked = false;

        private enum AnimState { Idle, Kick };
        private AnimationComponent<Knight> animComponent;

        private void Awake()
        {            
            animComponent = new AnimationComponent<Knight>(this, GetComponent<Animator>());

            App.Cache.userControl.OnKick(() => {
                if (!hasKicked && !IsPausedProperty.Value)
                {
                    animComponent.Play(AnimState.Kick);
                    hasKicked = true;
                }                
            });
        }


        public void PlayKickSwooshEffect()
        {
            Kernel.PfxService.PlayAt(pfxKickSwoosh, pfxSlotKickSwoosh.position);
        }


        public void OnKickAnimationEnd()
        {   
            MessageBroker.Default.Publish(JesterEffects.Kick);
            Kernel.AudioService.PlayRandomizedSFX(KickSound);
        }
    }
}
