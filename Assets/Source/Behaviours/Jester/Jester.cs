using Assets.Source.Behaviours.Jester.Components;
using Assets.Source.Config;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester
{
    public class Jester : AbstractBodyBehaviour
    {
        [SerializeField] private JesterSoundEffectsConfig soundEffectsConfig;

        [SerializeField] private JesterSpriteEffectsConfig spriteEffectsConfig;
        [SerializeField] private GameObject goSprite;


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


        private FlightRecorder flightRecorder;
        private JesterSounds soundEffects;
        private JesterSprite spriteEffects;

        private void Start()
        {            
            flightRecorder = new FlightRecorder(this);
            soundEffects = new JesterSounds(this, soundEffectsConfig);
            spriteEffects = new JesterSprite(this, goSprite, spriteEffectsConfig);
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
