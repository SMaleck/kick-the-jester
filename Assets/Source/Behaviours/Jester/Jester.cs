using UnityEngine;

namespace Assets.Source.Behaviours.Jester
{    
    public class Jester : AbstractBodyBehaviour
    {
        private CollisionListener _Collisions;
        public CollisionListener Collisions
        {
            get
            {
                if(_Collisions == null)
                {
                    _Collisions = GetBehaviour<CollisionListener>();
                }

                return _Collisions;
            }
        }


        // Utility to get behaviours on the Jester, since some might be on children
        public T GetBehaviour<T>()
        {
            T behaviour = GetComponent<T>();

            // Try to find in children if first attempt was unsuccessfull
            if(behaviour == null)
            {
                behaviour = GetComponentInChildren<T>();
            }

            return behaviour;
        }
    }
}
