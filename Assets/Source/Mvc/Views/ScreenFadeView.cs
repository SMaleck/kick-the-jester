using UniRx;
using UnityEngine;

namespace Assets.Source.Mvc.Views
{
    public class ScreenFadeView : AbstractView
    {
        [SerializeField] private CanvasGroup curtain;
        [SerializeField] private GameObject loadingScreen;

        public FloatReactiveProperty CurtainAlpha = new FloatReactiveProperty(0);
        public bool CurtainIsActive
        {
            get { return curtain.gameObject.activeSelf; }
            set { curtain.gameObject.SetActive(value); }
        }

        public bool LoadingScreenIsActive
        {
            get { return loadingScreen.gameObject.activeSelf; }
            set { loadingScreen.gameObject.SetActive(value); }
        }

        public override void Setup()
        {
            CurtainAlpha.Subscribe(e =>
            {
                curtain.alpha = Mathf.Clamp01(e);                

                CurtainIsActive = e >= 0.0001f;
                LoadingScreenIsActive = e >= 0.999f;

            }).AddTo(this);

            CurtainAlpha.Value = 0;
            loadingScreen.SetActive(false);
        }
    }
}
