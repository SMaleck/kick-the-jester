using UnityEngine;

namespace Assets.Source.Entities.Jester
{
    public class Jester : BaseEntity
    {
        private Rigidbody body;

        public void Start()
        {
            body = gameObject.GetComponent<Rigidbody>();
        }


        public void ApplyKick(Vector3 Force)
        {
            body.AddForce(Force);
        }
    }
}
