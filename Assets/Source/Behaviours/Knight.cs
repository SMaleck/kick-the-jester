using Assets.Source.App;
using Assets.Source.Behaviours.Jester;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours
{
    public class Knight : AbstractBehaviour
    {        
        [SerializeField] private AudioClip KickSound;
        private Animator animator;
        private bool hasKicked = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            App.Cache.userControl.OnKick(() => {
                if (!hasKicked)
                {
                    animator.Play("Anim_Knight_Kick");
                    hasKicked = true;
                }                
            });
        }


        public void OnKickAnimationEnd()
        {
            MessageBroker.Default.Publish(JesterEffects.Kick);
            Kernel.AudioService.PlayRandomizedSFX(KickSound);
        }
    }
}
