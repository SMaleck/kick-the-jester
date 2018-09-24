using Assets.Source.Entities.Jester.MonoComponents;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester
{
    public class JesterEntity : AbstractPausableMonoEntity
    {
        [SerializeField] public Collisions Collisions;
        [SerializeField] public GameObject GoBodySprite;
        [SerializeField] public GameObject GoProjectileSprite;

        [SerializeField] public Transform EffectSlotKick;
        [SerializeField] public Transform EffectSlotGround;

        private SpriteRenderer _bodySprite;
        public SpriteRenderer BodySprite => _bodySprite ?? (_bodySprite = GoBodySprite.GetComponent<SpriteRenderer>());

        private Animator _bodyAnimator;
        public Animator BodyAnimator => _bodyAnimator ?? (_bodyAnimator = GoBodySprite.GetComponent<Animator>());

        private Animator _projectileAnimator;
        public Animator ProjectileAnimator => _projectileAnimator ?? (_projectileAnimator = GoProjectileSprite.GetComponent<Animator>());

        private Rigidbody2D _goBody;
        public Rigidbody2D GoBody => _goBody ?? (_goBody = GetComponent<Rigidbody2D>());

        
        public ReactiveCommand OnKicked = new ReactiveCommand();
        public ReactiveCommand OnShot = new ReactiveCommand();
        public ReactiveCommand OnLanded = new ReactiveCommand();


        public override void Initialize()
        {            
        }
    }
}
