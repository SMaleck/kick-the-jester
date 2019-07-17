using UnityEngine;

namespace Assets.Source.Util
{
    public class AbstractMonoBehaviour : MonoBehaviour
    {
        public Transform GoTransform => gameObject.transform;
        public MonoBehaviour Disposer => this;

        public Vector3 Position
        {
            get { return GoTransform.position; }
            set { GoTransform.position = value; }
        }

        public Vector3 LocalPosition
        {
            get { return GoTransform.localPosition; }
            set { GoTransform.localPosition = value; }
        }
    }
}
