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
        /* -------------------------------------------------------------------------------------- */
        #region REACTIVE COMMANDS / PROPERTIES
        
        public ReactiveCommand OnStarted;        
        public ReactiveCommand OnLanded;

        public FloatReactiveProperty DistanceProperty = new FloatReactiveProperty(0);
        public float Distance
        {
            get { return DistanceProperty.Value; }
            set { DistanceProperty.Value = value; }
        }

        public FloatReactiveProperty HeightProperty = new FloatReactiveProperty(0);
        public float Height
        {
            get { return HeightProperty.Value; }
            set { HeightProperty.Value = value; }
        }

        public Vector2ReactiveProperty VelocityProperty = new Vector2ReactiveProperty();
        public Vector2 Velocity
        {
            get { return VelocityProperty.Value; }
            set { VelocityProperty.Value = value; }
        }

        public IntReactiveProperty CollectedCurrencyProperty = new IntReactiveProperty(0);
        public int CollectedCurrency
        {
            get { return CollectedCurrencyProperty.Value; }
            set { CollectedCurrencyProperty.Value = value; }
        }

        public IntReactiveProperty EarnedCurrencyProperty = new IntReactiveProperty(0);
        public int EarnedCurrency
        {
            get { return EarnedCurrencyProperty.Value; }
            set { EarnedCurrencyProperty.Value = value; }
        }

        #endregion


        /* -------------------------------------------------------------------------------------- */
        #region COMPONENTS        

        [SerializeField] private JesterSoundEffectsConfig soundEffectsConfig;
        [SerializeField] private JesterSpriteEffectsConfig spriteEffectsConfig;
        [SerializeField] private GameObject goSprite;


        private CollisionListener _Collisions;
        public CollisionListener Collisions
        {
            get
            {
                if (_Collisions == null)
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

        #endregion


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
