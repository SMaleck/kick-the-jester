using Assets.Source.Entities.Jester.MonoComponents;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester
{
    public class JesterEntity : AbstractMonoEntity
    {
        [SerializeField] public Collisions Collisions;
        [SerializeField] public GameObject GoBodySprite;
        [SerializeField] public GameObject GoEffectSprite;

        [SerializeField] public Transform EffectSlotKick;
        [SerializeField] public Transform EffectSlotGround;

        private SpriteRenderer _bodySprite;
        public SpriteRenderer BodySprite => _bodySprite ?? (_bodySprite = GoBodySprite.GetComponent<SpriteRenderer>());

        private Rigidbody2D _goBody;
        public Rigidbody2D GoBody => _goBody ?? (_goBody = GoBodySprite.GetComponent<Rigidbody2D>());

        // todo on kicked needs to be reliable
        public ReactiveCommand OnKicked = new ReactiveCommand();
        public ReactiveCommand OnShot = new ReactiveCommand();

        

        public override void Initialize()
        {
            
        }
    }
}
