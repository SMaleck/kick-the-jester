using UnityEngine;

namespace Assets.Source.Entities.Items
{
    public class Obstacle : AbstractItem
    {        
        [Range (0.0f, 1.0f)]
        public float StoppingPowerPercent = 1f;
        
        public void OnTriggerEnter2D(Collider2D collision)
        {
            Rigidbody2D body;
            if (!TryGetBody(collision, out body))
            {
                return;
            }

            float VelocityReductionAmount = body.velocity.magnitude - (body.velocity.magnitude * StoppingPowerPercent);
            body.velocity = body.velocity.normalized * VelocityReductionAmount;

            // Disable this trigger, so that we don't slow down multiple times unintentionally
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
