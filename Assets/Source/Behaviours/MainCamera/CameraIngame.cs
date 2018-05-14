using UnityEngine;

namespace Assets.Source.Behaviours.MainCamera
{
    public class CameraIngame : CameraBase, ICamera
    {
        private Vector2 origin;
        private Transform jesterTransform;


        protected override void Start()
        {
            base.Start();

            // Setup Following
            origin = goTransform.position;
            jesterTransform = App.Cache.jester.goTransform;
        }


        protected virtual void Update()
        {
            FollowJester();
        }        


        private void FollowJester()
        {
            Vector3 TPos = jesterTransform.position;
            goTransform.position = new Vector3(TPos.x, Mathf.Clamp(TPos.y, origin.y, float.MaxValue), goTransform.position.z);
        }
    }
}
