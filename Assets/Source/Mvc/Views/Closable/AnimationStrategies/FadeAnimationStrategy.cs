using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Source.Mvc.Views.Closable.AnimationStrategies
{
    public class FadeAnimationStrategy : IIClosableViewAnimationStrategy
    {
        private const float TransitionTimeSeconds = 0.5f;

        private readonly Transform _target;
        private readonly CanvasGroup _canvasGroup;

        public FadeAnimationStrategy(Transform target, CanvasGroup canvasGroup)
        {
            _target = target;
            _canvasGroup = canvasGroup;
        }

        public void Open(Action onComplete)
        {
            _target.gameObject.SetActive(true);
            _canvasGroup.alpha = 0;

            _canvasGroup
                .DOFade(1f, TransitionTimeSeconds)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() => onComplete?.Invoke());
        }

        public void Close(Action onComplete)
        {
            DOTween.Sequence()
                .Append(_canvasGroup
                    .DOFade(0f, TransitionTimeSeconds)
                    .SetEase(Ease.InOutCubic))
                .AppendCallback(() => _target.gameObject.SetActive(false))
                .AppendCallback(() => onComplete?.Invoke());
        }
    }
}
