using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using DG.Tweening;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class ScreenFadeController : AbstractController
    {
        private readonly ScreenFadeView _view;
        private readonly SceneTransitionService _sceneTransitionService;

        private readonly float _fadeSeconds;
        private Tweener _fadeTweener;

        public ScreenFadeController(ScreenFadeView view, SceneTransitionService sceneTransitionService)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _sceneTransitionService = sceneTransitionService;
            _fadeSeconds = SceneTransitionService.LOADING_GRACE_PERIOD_SECONDS;

            _sceneTransitionService.OnSceneLoadStarted
                .Subscribe(_ => Fade(0, 1))
                .AddTo(Disposer);

            _sceneTransitionService.OnSceneInitComplete
                .Subscribe(_ => Fade(1, 0))
                .AddTo(Disposer);
        }


        private void Fade(float from, float to)
        {
            _fadeTweener?.Kill();

            _fadeTweener = DOTween.To(value => _view.CurtainAlpha.Value = value, from, to, _fadeSeconds)
                .SetEase(Ease.InCubic);
        }
    }
}
