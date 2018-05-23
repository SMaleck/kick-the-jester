using Assets.Source.App;
using Assets.Source.Behaviours.MainCamera.Components;
using Assets.Source.Models;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.MainCamera
{
    public class CameraBase : AbstractBehaviour, ICamera
    {        
        public Camera UCamera { get; protected set; }

        [SerializeField] protected GameObject pfSkyFade;
        [SerializeField] protected SpriteRenderer screenFaderSprite;

        protected ScreenFader screenFader;
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

            if (!Kernel.Ready.Value)
            {
                Kernel.Ready.Where(e => e).Subscribe(_ => SetupFader()).AddTo(this);
            }
            else
            {
                SetupFader();
            }            
        }

        private void SetupFader()
        {
            screenFader = new ScreenFader(this, screenFaderSprite);
            FadeIn(null);
        }


        private void Start()
        {
            // Setup following component, if jester is present in the scene
            try
            {
                if (App.Cache.Jester != null)
                {
                    followTarget = new FollowTarget(this, App.Cache.Jester.goTransform);


                    // Setup Sky Gradient
                    var goSkyFade = GameObject.Instantiate(pfSkyFade);
                    goSkyFade.transform.SetParent(this.transform);
                }
            }
            catch { }
        }


        public void FadeIn(NotifyEventHandler callback)
        {
            screenFader.FadeIn(callback);
        }


        public void FadeOut(NotifyEventHandler callback)
        {
            screenFader.FadeOut(callback);
        }
    }
}
