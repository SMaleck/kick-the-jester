using UnityEngine;

namespace Assets.Source.GameLogic.Components
{
    public class CameraScript : MonoBehaviour
    {
        // Exposed in Editor
        public GameObject Target;
        public Transform TargetTransform;

        private Transform OwnerTransform;


        // Use this for initialization
        void Start()
        {
            OwnerTransform = gameObject.transform;
            TargetTransform = Target.GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            FollowTarget();
        }


        private void FollowTarget()
        {
            if (Target != null)
            {
                Vector3 TPos = TargetTransform.position;

                OwnerTransform.position = new Vector3(TPos.x, TPos.y, OwnerTransform.position.z);
            }
        }
    }
}