using UnityEngine;

namespace Assets.Source.Entities.Behaviours
{
    public class FollowJester : BaseEntity
    {
        private Vector2 origin;
        private Transform target;

        public bool FollowX = true;
        public bool FollowY = false;
        public bool KeepRelativeDistance = true;

        private float offsetX = 0;
        private float offsetY = 0;

        void Start()
        {
            target = App.Cache.jester.goTransform;

            origin = goTransform.position;

            if (KeepRelativeDistance)
            {
                offsetX = origin.x - target.position.x;
                offsetY = origin.y - target.position.y;
            }           
        }

        
        void LateUpdate()
        {
            Vector3 TPos = target.position;

            float nextX = FollowX ? target.position.x : origin.x;
            float nextY = FollowY ? target.position.y : origin.y;

            goTransform.position = new Vector3(nextX + offsetX, nextY + offsetY, gameObject.transform.position.z);
        }        
    }
}