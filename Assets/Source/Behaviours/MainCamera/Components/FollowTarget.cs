using UnityEngine;

namespace Assets.Source.Behaviours.MainCamera.Components
{
    public class FollowTarget : AbstractComponent<CameraBase>
    {
        private Vector2 origin;
        private Transform target;        

        private float offsetX = 3.5f;      

        public FollowTarget(CameraBase owner, Transform target)
            : base(owner)
        {            
            origin = owner.goTransform.position;
            this.target = target;
        }


        protected override void Update()
        {
            Vector3 TPos = target.position;
            owner.goTransform.position = new Vector3(TPos.x + offsetX, Mathf.Clamp(TPos.y, origin.y, float.MaxValue), owner.goTransform.position.z);
        }
    }
}
