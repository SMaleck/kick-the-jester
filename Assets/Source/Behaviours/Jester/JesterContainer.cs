using Assets.Source.App;
using Assets.Source.Behaviours.Jester.Components;
using Assets.Source.Config;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester
{
    public enum JesterEffects { Kick, Shot, Impact }

    public class JesterContainer : AbstractBodyBehaviour
    {
        /* -------------------------------------------------------------------------------------- */
        #region ANIMATOINS & EFFECTS

        [SerializeField] private Animator animator;
        [SerializeField] public Transform Slot_GroundTouch;
        [SerializeField] public Transform Slot_KickTouch;

        #endregion


        /* -------------------------------------------------------------------------------------- */
        #region REACTIVE PROPERTIES

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

        public FloatReactiveProperty RelativeKickForceProperty = new FloatReactiveProperty(0f);
        public float RelativeKickForce
        {
            get { return RelativeKickForceProperty.Value; }
            set { RelativeKickForceProperty.Value = value; }
        }

        public FloatReactiveProperty RelativeVelocityProperty = new FloatReactiveProperty(0f);
        public float RelativeVelocity
        {
            get { return RelativeVelocityProperty.Value; }
            set { RelativeVelocityProperty.Value = value; }
        }

        public IntReactiveProperty AvailableShotsProperty = new IntReactiveProperty(0);
        public int AvailableShots
        {
            get { return AvailableShotsProperty.Value; }
            set { AvailableShotsProperty.Value = value; }
        }

        #endregion


        /* -------------------------------------------------------------------------------------- */
        #region COMPONENTS        

        [SerializeField] private JesterSoundEffectsConfig soundEffectsConfig;
        [SerializeField] private JesterSpriteEffectsConfig spriteEffectsConfig;
        [SerializeField] private GameObject goJesterSprite;
        [SerializeField] private GameObject goEffectSprite;


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
        private SoundEffect soundEffect;
        private SpriteEffect spriteEffect;
        private MotionBoot motionBoot;
        private MotionShoot motionShoot;

        #endregion        


        private void Awake()
        {
            flightRecorder = new FlightRecorder(this);
            soundEffect = new SoundEffect(this, soundEffectsConfig, App.Cache.Kernel.AudioService);
            spriteEffect = new SpriteEffect(this, animator, goJesterSprite, goEffectSprite, spriteEffectsConfig, App.Cache.Kernel.PfxService);
            motionBoot = new MotionBoot(this, new MotionConfig(), App.Cache.GameLogic, App.Cache.userControl);
            motionShoot = new MotionShoot(this, new MotionShootConfig(), App.Cache.GameLogic, App.Cache.userControl);

            // Listen to Pause State
            App.Cache.Kernel.AppState.IsPausedProperty
                           .Subscribe(OnPauseStateChanged)
                           .AddTo(this);            
        }

        
        private void OnPauseStateChanged(bool IsPaused)
        {
            goBody.simulated = !IsPaused;
        }
    }
}
