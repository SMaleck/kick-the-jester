using Assets.Source.App;
using Assets.Source.Behaviours.Jester.Components;
using Assets.Source.Config;
using Assets.Source.GameLogic;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester
{
    public class Jester : AbstractBodyBehaviour
    {
        /* -------------------------------------------------------------------------------------- */
        #region REACTIVE COMMANDS / PROPERTIES

        public BoolReactiveProperty IsStartedProperty = new BoolReactiveProperty(false);        
        public BoolReactiveProperty IsLandedProperty = new BoolReactiveProperty(false);        

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
<<<<<<< HEAD

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

        public FloatReactiveProperty RelativeKickForceProperty = new FloatReactiveProperty(0f);
        public float RelativeKickForce
=======
        
        public IntReactiveProperty RelativeKickForceProperty = new IntReactiveProperty(0);
        public int RelativeKickForce
>>>>>>> cd60d76b4c539867612ad4908d6424cf33864655
        {
            get { return RelativeKickForceProperty.Value; }
            set { RelativeKickForceProperty.Value = value; }
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

        private void Awake()
        {
            flightRecorder = new FlightRecorder(this);
            soundEffects = new SoundEffect(this, soundEffectsConfig);
            spriteEffects = new SpriteEffect(this, goSprite, spriteEffectsConfig);
            kickForce = new KickForce(this, Kernel.PlayerProfileService.KickCount);

            // Listen to Pause State
            App.Cache.GameStateMachine.StateProperty                                      
                                      .Subscribe(OnPauseStateChanged)
                                      .AddTo(this);
        }

        
        private void OnPauseStateChanged(GameState state)
        {
            goBody.simulated = !state.Equals(GameState.Paused);
        }
    }
}
