using UnityEngine;

namespace Assets.Source.Entities
{
    public class Jester : BaseEntity
    {
        private Rigidbody2D _Body;
        public Rigidbody2D Body
        {
            get
            {
                if(_Body == null)
                {
                    _Body = gameObject.GetComponent<Rigidbody2D>();
                }

                return _Body;
            }
        }


        public void OnGroundHit()
        {

        }
    }
}
