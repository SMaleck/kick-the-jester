using UnityEngine;

namespace Assets.Source.Items
{
    public class Boost : AbstractItem
    {
        [Range(0.0f, float.MaxValue)]
        public float Strength = 5f;

        public ForceMode forceMode;

        public Vector2 Direction = new Vector2(1, 1);

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Rigidbody2D body;
            if(!TryGetBody(collision, out body))
            {
                return;
            }

            Vector2 force = Direction * Strength;
            body.AddForce(ApplyForceMode(force, forceMode, body.mass));

            // Disable this trigger
            gameObject.GetComponent<Collider2D>().enabled = false;
        }

        Vector2 ApplyForceMode(Vector2 force, ForceMode forceMode, float mass)
        {
            switch (forceMode)
            {
                case ForceMode.Force:
                    return force;
                case ForceMode.Impulse:
                    return force / Time.fixedDeltaTime;
                case ForceMode.Acceleration:
                    return force * mass;
                case ForceMode.VelocityChange:
                    return force * mass / Time.fixedDeltaTime;
            }

            return force;
        }
    }
}
