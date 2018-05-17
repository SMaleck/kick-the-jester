using Assets.Source.Behaviours.Jester.Components;
using Assets.Source.Config;
using Assets.Source.Repositories;
using UniRx;
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
                    _Collisions = GetComponentInChildren<CollisionListener>();
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

            // Listen to Pause State
            App.Cache.RepoRx.GameStateRepository.StateProperty
                                                .TakeUntilDestroy(this)
                                                .Subscribe(OnPauseStateChanged);            
        }

        
        private void OnPauseStateChanged(GameState state)
        {
            goBody.simulated = !state.Equals(GameState.Paused);
        }
    }
}
