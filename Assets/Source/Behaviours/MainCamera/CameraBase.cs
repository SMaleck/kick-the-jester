using Assets.Source.Behaviours.MainCamera.Components;
using UnityEngine;

namespace Assets.Source.Behaviours.MainCamera
{
    public class CameraBase : AbstractBehaviour, ICamera
    {        
        public Camera UCamera { get; protected set; }
        
        [SerializeField] protected SpriteRenderer screenFaderSprite;
        
        protected FollowTarget followTarget;


        protected float _cameraWidth;
        public float Width
        {
            get
            {
                if (_cameraWidth <= 0)
                {
                    _cameraWidth = Height * UCamera.aspect;
                }

                return _cameraWidth;
            }
        }

        protected float _cameraHeight;
        public float Height
        {
            get
            {
                if (_cameraHeight <= 0)
                {
                    _cameraHeight = 2f * UCamera.orthographicSize;
                }

                return _cameraHeight;
            }
        }


        protected virtual void Awake()
        {
            UCamera = GetComponent<Camera>();           
        }


        private void Start()
        {
            // Setup following component, if jester is present in the scene
            try
            {
                if (App.Cache.Jester != null)
                {
                    followTarget = new FollowTarget(this, App.Cache.Jester);
                }
            }
            catch { }
        }
    }
}
