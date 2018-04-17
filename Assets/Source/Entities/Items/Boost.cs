using UnityEngine;

namespace Assets.Source.Entities.Items
{
    public class Boost : AbstractItem
    {
        [Range(0.0f, float.MaxValue)]
        public float Strength = 5f;

        public Vector2 Direction = new Vector2(1, 1);

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Rigidbody2D body;
            if(!TryGetBody(collision, out body))
            {
                return;
            }

            Vector2 force = Direction * Strength;
            body.AddForce(force);

            // Disable this trigger
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
