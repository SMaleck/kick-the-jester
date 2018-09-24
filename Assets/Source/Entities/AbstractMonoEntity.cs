using UnityEngine;
using Zenject;

namespace Assets.Source.Entities
{
    public abstract class AbstractMonoEntity : MonoBehaviour, IInitializable
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

        // ToDo Figure out how to run this automatically through Zenject
        private void Start()
        {
            Initialize();
        }

        public abstract void Initialize();
    }
}
