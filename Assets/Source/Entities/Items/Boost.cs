using Assets.Source.Entities.Jester;
using UnityEngine;

namespace Assets.Source.Entities.Items
{
    public class Boost : AbstractItemEntity
    {
        [Range(0.0f, float.MaxValue)]
        public float Strength = 5f;

        [SerializeField] public ForceMode forceMode;
        public Vector2 Direction = new Vector2(1, 1);

        protected override void Setup() { }

        protected override void Execute(JesterEntity jester)
        {            
            Vector2 force = Direction * Strength;

            jester.GoBody.AddForce(ApplyForceMode(force, forceMode, jester.GoBody.mass));
        }


        private Vector2 ApplyForceMode(Vector2 force, ForceMode forceMode, float mass)
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
