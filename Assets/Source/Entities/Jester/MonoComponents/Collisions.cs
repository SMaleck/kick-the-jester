using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Jester.MonoComponents
{
    public class Collisions : AbstractMonoEntity
    {
        private const string TAG_GROUND = "Ground";
        private const string TAG_OBSTACLE = "Obstacle";
        private const string TAG_BOOST = "Boost";
        private const string TAG_PICKUP = "Pickup";

        public ReactiveCommand OnGround = new ReactiveCommand();
        public ReactiveCommand OnBoost = new ReactiveCommand();
        public ReactiveCommand OnPickup = new ReactiveCommand();
        public ReactiveCommand OnObstacle = new ReactiveCommand();

        public override void Initialize()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case TAG_GROUND:
                    OnGround.Execute();
                    break;

                case TAG_OBSTACLE:
                    OnObstacle.Execute();
                    break;

                case TAG_BOOST:
                    OnBoost.Execute();
                    break;

                case TAG_PICKUP:
                    OnPickup.Execute();
                    break;
            }
        }
    }
}
