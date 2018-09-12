using UniRx;
using UnityEngine;

namespace Assets.Source.Mvc.Views
{
    public class ScreenFadeView : AbstractView
    {
        [SerializeField] private CanvasGroup curtain;
        [SerializeField] private GameObject loadingScreen;

        public FloatReactiveProperty CurtainAlpha = new FloatReactiveProperty(0);        

        public override void Initialize()
        {
            CurtainAlpha.Subscribe(e =>
            {
                curtain.alpha = Mathf.Clamp01(e);
                loadingScreen.SetActive(e >= 0.999f);

            }).AddTo(this);

            CurtainAlpha.Value = 0;
            loadingScreen.SetActive(false);
        }
    }
}
