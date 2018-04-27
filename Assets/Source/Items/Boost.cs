using Assets.Source.Behaviours.Jester;
using UnityEngine;

namespace Assets.Source.Items
{
    public class Boost : AbstractItem
    {
        [Range(0.0f, float.MaxValue)]
        public float Strength = 5f;

        public ForceMode forceMode;
        public Vector2 Direction = new Vector2(1, 1);


        protected override void Execute(Jester jester)
        {            
            Vector2 force = Direction * Strength;

            jester.goBody.AddForce(ApplyForceMode(force, forceMode, jester.goBody.mass));
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
