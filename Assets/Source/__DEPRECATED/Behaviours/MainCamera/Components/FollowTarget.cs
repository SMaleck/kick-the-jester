using Assets.Source.Behaviours.Jester;
using UnityEngine;
using UniRx;

namespace Assets.Source.Behaviours.MainCamera.Components
{
    public class FollowTarget : AbstractComponent<CameraBase>
    {
        private Vector2 origin;
        private readonly JesterContainer jester;
        private Transform target;        

        private float offsetX = 3.5f;

        private bool increaseOffset = false;
        private float offsetXOnLanding = 5.5f;

        public FollowTarget(CameraBase owner, JesterContainer jester)
            : base(owner)
        {
            this.jester = jester;
            origin = owner.goTransform.position;
            this.target = jester.transform;

            jester.IsLandedProperty
                  .Where(e => e)
                  .Subscribe(_ => increaseOffset = true)
                  .AddTo(owner);
        }


        protected override void Update()
        {
            Vector3 TPos = target.position;
            owner.goTransform.position = new Vector3(TPos.x + offsetX, Mathf.Clamp(TPos.y, origin.y, float.MaxValue), owner.goTransform.position.z);

            if (increaseOffset)
            {
                offsetX += 3 * Time.deltaTime;
                increaseOffset = offsetX < offsetXOnLanding;
            }
        }
    }
}
