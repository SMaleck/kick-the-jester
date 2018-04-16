using UnityEngine;

namespace Assets.Source.Entities
{
    public class Obstacle : BaseEntity
    {
        // Range [0, 1]
        public float StoppingPowerPercent = 1f;
        
        public void OnTriggerEnter2D(Collider2D collision)
        {
            Rigidbody2D otherBody = collision.gameObject.GetComponent<Rigidbody2D>();

            if(otherBody == null)
            {
                return;
            }

            float VelocityReductionAmount = otherBody.velocity.magnitude - (otherBody.velocity.magnitude * StoppingPowerPercent);            
            otherBody.velocity = otherBody.velocity.normalized * VelocityReductionAmount;

            // Disaable this trigger, so that we don't slow down multiple times unintentionally
            this.enabled = false;
        }
    }
}
