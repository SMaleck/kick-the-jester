using Assets.Source.Config;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class SpriteEffect : AbstractComponent<Jester>
    {
        private readonly GameObject goSprite;
        private readonly JesterSpriteEffectsConfig config;

        private bool listenForImpacts = false;
        private bool isRotating = false;

        private float currentRotationSpeed = 0;
        private Vector3 rotationDirection = new Vector3(0, 0, -1);


        public SpriteEffect(Jester owner, GameObject goSprite, JesterSpriteEffectsConfig config)
            : base(owner, true)
        {
            this.goSprite = goSprite;
            this.config = config;

            owner.OnStarted.Subscribe(_ => { listenForImpacts = true; }).AddTo(owner);
            owner.OnLanded.Subscribe(_ => { listenForImpacts = isRotating = false; }).AddTo(owner);

            owner.Collisions.OnGround(SetRotation);            
        }


        protected override void Update()
        {
            if (isRotating)
            {
                goSprite.transform.Rotate(rotationDirection * currentRotationSpeed * Time.deltaTime);
            }
        }


        private void SetRotation()
        {
            if (listenForImpacts)
            {
                isRotating = true;
                currentRotationSpeed = UnityEngine.Random.Range(config.MinRotationSpeed, config.MaxRotationSpeed);
            }
        }
    }
}
