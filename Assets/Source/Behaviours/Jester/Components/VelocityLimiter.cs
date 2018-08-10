using Assets.Source.App;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class VelocityLimiter : AbstractComponent<JesterContainer>
    {
        private readonly float maxVelocityX;
        private readonly float maxVelocityY;       

        private float velocityX
        {
            get { return owner.goBody.velocity.x; }
        }

        private float velocityY
        {
            get { return owner.goBody.velocity.y; }
        }
        

        public VelocityLimiter(JesterContainer owner, float maxVelocityX, float maxVelocityY, bool isPausable = true) 
            : base(owner, isPausable)
        {            
            this.maxVelocityX = maxVelocityX;
            this.maxVelocityY = maxVelocityY;
        }


        protected override void LateUpdate()
        {
            float nextVelocityX = velocityX.Clamp(0, maxVelocityX);
            float nextVelocityY = velocityY.Clamp(-float.MaxValue, maxVelocityY);

            SetVelocity(nextVelocityX, nextVelocityY);

            owner.RelativeVelocity = owner.goBody.velocity.x.AsRelativeTo1(maxVelocityX);
        }


        private void SetVelocity(float x, float y)
        {
            owner.goBody.velocity = new Vector2(x, y);
        }
    }
}
