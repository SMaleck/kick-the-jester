using UnityEngine;

namespace Assets.Source.Entities
{
    public class FollowCamera : BaseEntity
    {
        private Vector2 origin;
        private Transform target;

        
        void Start()
        {
            origin = goTransform.position;
            target = App.Cache.jester.goTransform;
        }
        
        void Update()
        {
            Vector3 TPos = target.position;

            goTransform.position = new Vector3(TPos.x, Mathf.Clamp(TPos.y, origin.y, float.MaxValue), goTransform.position.z);
        }
    }
}