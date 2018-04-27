using Assets.Source.Behaviours.Jester;
using UnityEngine;

namespace Assets.Source.Items
{
    public class Obstacle : AbstractItem
    {        
        [Range (0.0f, 1.0f)]
        public float StoppingPowerPercent = 1f;

        protected override void Execute(Jester jester)
        {
            Rigidbody2D body = jester.goBody;

            float VelocityReductionAmount = body.velocity.magnitude - (body.velocity.magnitude * StoppingPowerPercent);
            body.velocity = body.velocity.normalized * VelocityReductionAmount;

            // Remove bouncy material, so Jester will not bounce on land
            jester.GetComponent<Collider2D>().sharedMaterial = null;
        }
    }
}
