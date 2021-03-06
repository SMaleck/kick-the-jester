﻿using Assets.Source.Mvc.Views;
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

            _sceneTransitionService.State
                .Subscribe(OnTransitionStateChange)
                .AddTo(Disposer);
        }


        private void OnTransitionStateChange(TransitionState state)
        {
            switch (state)
            {
                case TransitionState.Before:
                    Fade(0, 1);
                    break;

                case TransitionState.After:
                    Fade(1, 0);
                    break;

                default:
                    return;
            }
        }


        private void Fade(float from, float to)
        {
            _fadeTweener?.Kill();

            _fadeTweener = DOTween.To(value => _view.CurtainAlpha.Value = value, from, to, _fadeSeconds)
                .SetEase(Ease.InCubic);
        }
    }
}
