using UnityEngine;

namespace Assets.Source.Entities
{
    public class Jester : BaseEntity
    {
        private Rigidbody2D body;


        public void Start()
        {
            body = gameObject.GetComponent<Rigidbody2D>();
        }


        public void ApplyKick(Vector3 Force)
        {
            body.AddForce(Force);
        }
    }
}
