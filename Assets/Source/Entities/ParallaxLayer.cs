using UnityEngine;

namespace Assets.Source.Entities
{
    public class ParallaxLayer : BaseEntity
    {
        private ParallaxLayer prlxCopy;

        private Rigidbody2D body;
        public float VelocityFactor = 1f;
        public bool Loop = true;

        public void Awake()
        {
            body = gameObject.GetComponent<Rigidbody2D>();            
        }


        // Creates a copy of its self needed for looping
        public void CreateOffsettedCopy()
        {
            prlxCopy = GameObject.Instantiate(gameObject).GetComponent<ParallaxLayer>();
            prlxCopy.OffsetSelf();
        }


        // Offsets its own position to the right by its own width
        // Needed for looping
        public void OffsetSelf()
        {
            float width = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
            Vector3 offsetPos = new Vector3(goTransform.position.x + width, goTransform.position.y, goTransform.position.z);

            goTransform.position = offsetPos;
        }


        // Sets the velocity for parallax movement
        public void SetVelocity(Vector2 Velocity)
        {
            body.velocity.Set(Velocity.x * VelocityFactor, Velocity.y * VelocityFactor);

            if(prlxCopy != null)
            {
                prlxCopy.SetVelocity(Velocity);
            }
        }
    }
}
