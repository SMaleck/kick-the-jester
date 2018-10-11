using Assets.Source.Entities.Jester;
using UnityEngine;

namespace Assets.Source.Entities.Items
{
    public class Obstacle : AbstractItemEntity
    {        
        [Range (0.0f, 1.0f)]
        public float StoppingPowerPercent = 1f;

        public override void Initialize() { }

        protected override void Execute(JesterEntity jester)
        {
            Rigidbody2D body = jester.GoBody;

            float velocityReductionAmount = body.velocity.magnitude - (body.velocity.magnitude * StoppingPowerPercent);
            body.velocity = body.velocity.normalized * velocityReductionAmount;

            // Remove bouncy material, so Jester will not bounce on land
            if (StoppingPowerPercent >= 1)
            {
                jester.GetComponent<Collider2D>().sharedMaterial = null;
            }
        }
    }
}
