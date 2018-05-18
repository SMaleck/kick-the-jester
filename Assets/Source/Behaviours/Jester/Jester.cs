using Assets.Source.AppKernel;
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
        private SoundEffect soundEffects;
        private SpriteEffect spriteEffects;
        private KickForce kickForce;

        private void Start()
        {
            flightRecorder = new FlightRecorder(this);
            soundEffects = new SoundEffect(this, soundEffectsConfig);
            spriteEffects = new SpriteEffect(this, goSprite, spriteEffectsConfig);
            kickForce = new KickForce(this, Kernel.PlayerProfileService.KickCount);

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
