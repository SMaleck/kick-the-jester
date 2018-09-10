using System;


namespace Assets.Source.Mvc
{
    public class ScreenFadeController : AbstractController
    {
        private readonly ScreenFadeView _view;

        private readonly float fadeSeconds = 0.6f;


        public ScreenFadeController(ScreenFadeView view)
        {
            _view = view;
        }


        public void ToBlack(Action onComplete = null)
        {
            Fade(0, 1, onComplete);
        }


        public void ToWhite(Action onComplete = null)
        {
            Fade(1, 0, onComplete);
        }


        private void Fade(float from, float to, Action onComplete = null)
        {
            LTDescr ltd = LeanTween.value(from, to, fadeSeconds)
                                   .setEaseInCubic()
                                   .setOnUpdate((float alpha) => { _view.CurtainAlpha.Value = alpha; });

            if (onComplete != null)
            {
                ltd.setOnComplete(onComplete);
            }
        }
    }
}
