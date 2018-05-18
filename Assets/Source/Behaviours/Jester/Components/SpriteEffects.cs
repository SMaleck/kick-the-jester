using Assets.Source.Config;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class JesterSprite : AbstractJesterComponent
    {
        private readonly GameObject goSprite;
        private readonly JesterSpriteEffectsConfig config;

        private bool ListenForImpacts = false;
        private bool isRotating = false;

        private float currentRotationSpeed = 0;
        private Vector3 rotationDirection = new Vector3(0, 0, -1);


        public JesterSprite(Jester owner, GameObject goSprite, JesterSpriteEffectsConfig config)
            : base(owner, true)
        {
            this.goSprite = goSprite;
            this.config = config;

            App.Cache.RepoRx.JesterStateRepository.OnStartedFlight(
                () => { ListenForImpacts = true; });

            App.Cache.RepoRx.JesterStateRepository.OnLanded(
                () => { isRotating = false; });

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
            if (ListenForImpacts)
            {
                isRotating = true;
                currentRotationSpeed = UnityEngine.Random.Range(config.MinRotationSpeed, config.MaxRotationSpeed);
            }
        }
    }
}
