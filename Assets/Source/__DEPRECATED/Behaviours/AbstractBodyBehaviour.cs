using UnityEngine;

namespace Assets.Source.Behaviours
{
    public abstract class AbstractBodyBehaviour : AbstractBehaviour
    {
        protected Rigidbody2D _goBody;
        public Rigidbody2D goBody
        {
            get
            {
                if (_goBody == null)
                {
                    _goBody = gameObject.GetComponent<Rigidbody2D>();
                }

                return _goBody;
            }
        }
    }
}
