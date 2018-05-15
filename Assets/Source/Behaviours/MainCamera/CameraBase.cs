using Assets.Source.Models;
using UnityEngine;

namespace Assets.Source.Behaviours.MainCamera
{
    public class CameraBase : AbstractBehaviour, ICamera
    {        
        public Camera UCamera { get; protected set; }

        [SerializeField] protected GameObject prefabLoadingScreen;
        protected ScreenFader loadingScreen;


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

            // Setup Loading Screen
            var goLoadingScreen = GameObject.Instantiate(prefabLoadingScreen);
            goLoadingScreen.transform.SetParent(this.transform);
            loadingScreen = goLoadingScreen.GetComponent<ScreenFader>();            
        }


        protected virtual void Start()
        {
            FadeIn(null);
        }


        public void FadeIn(NotifyEventHandler callback)
        {
            loadingScreen.FadeIn(callback);
        }


        public void FadeOut(NotifyEventHandler callback)
        {
            loadingScreen.FadeOut(callback);
        }
    }
}
