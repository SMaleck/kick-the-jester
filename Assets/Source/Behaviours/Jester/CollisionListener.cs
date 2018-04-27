using Assets.Source.App;
using Assets.Source.Models;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester
{
    public class CollisionListener : AbstractBodyBehaviour
    {
        private event NotifyEventHandler _OnGround = delegate { };
        public void OnGround(NotifyEventHandler handler)
        {
            _OnGround += handler;
        }

        private event NotifyEventHandler _OnBoost = delegate { };
        public void OnBoost(NotifyEventHandler handler)
        {
            _OnBoost += handler;
        }

        private event NotifyEventHandler _OnPickup = delegate { };
        public void OnPickup(NotifyEventHandler handler)
        {
            _OnPickup += handler;
        }

        private event NotifyEventHandler _OnObstacle = delegate { };
        public void OnObstacle(NotifyEventHandler handler)
        {
            _OnObstacle += handler;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case Constants.TAG_GROUND:
                    _OnGround();
                    break;

                case Constants.TAG_OBSTACLE:
                    _OnObstacle();
                    break;

                case Constants.TAG_BOOST:
                    _OnBoost();
                    break;

                case Constants.TAG_PICKUP:
                    _OnPickup();
                    break;
            }
        }
    }
}
