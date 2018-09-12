using System;
using Assets.Source.Services;
using Assets.Source.Mvc.Views;
using UniRx;


namespace Assets.Source.Mvc
{
    public class ScreenFadeController : AbstractController
    {
        private readonly ScreenFadeView _view;
        private SceneTransitionService _sceneTransitionService;

        private readonly float _fadeSeconds;


        public ScreenFadeController(ScreenFadeView view, SceneTransitionService sceneTransitionService)
        {
            _view = view;
            _sceneTransitionService = sceneTransitionService;

            _fadeSeconds = SceneTransitionService.LOADING_GRACE_PERIOD_SECONDS;

            _sceneTransitionService.State
                .Subscribe(OnTransitionStateChange)
                .AddTo(Disposer);
        }


        private void OnTransitionStateChange(TransitionState state)
        {
            switch (state)
            {
                case TransitionState.Before:
                    ToBlack();
                    break;

                case TransitionState.After:
                    ToWhite();
                    break;

                default:
                    return;
            }
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
            LTDescr ltd = LeanTween.value(from, to, _fadeSeconds)
                                   .setEaseInCubic()
                                   .setOnUpdate((float alpha) => { _view.CurtainAlpha.Value = alpha; });

            if (onComplete != null)
            {
                ltd.setOnComplete(onComplete);
            }
        }
    }
}
