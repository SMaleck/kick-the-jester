using UnityEngine;

namespace Assets.Source.Util
{
    public class AbstractMonoBehaviour : AbstractDisposableMonoBehaviour
    {
        public Transform GoTransform => gameObject.transform;

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
