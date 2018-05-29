using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours
{
    public class AnimationComponent<T> : AbstractComponent<T> where T : AbstractBehaviour
    {
        public readonly Animator animator;

        public AnimationComponent(T owner, Animator animator, bool isPausable = true) 
            : base(owner, isPausable)
        {
            this.animator = animator;

            owner.IsPausedProperty.Subscribe((bool isPaused) => { animator.enabled = !isPaused; }).AddTo(owner);
        }


        public void Play(string animationName)
        {
            animator.Play(animationName);
        }
    }
}
