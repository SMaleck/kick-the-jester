using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Source.Mvc.Views.Closable.AnimationStrategies
{
    public class FadeAnimationStrategy : IIClosableViewAnimationStrategy
    {
        private const float TransitionTimeSeconds = 0.2f;

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
            _target.localScale = Vector3.zero;

            DOTween.Sequence()
                .Append(_canvasGroup.DOFade(1, TransitionTimeSeconds))
                .AppendCallback(() => onComplete?.Invoke());
        }

        public void Close(Action onComplete)
        {
            DOTween.Sequence()
                .Append(_canvasGroup.DOFade(0, TransitionTimeSeconds))
                .AppendCallback(() => _target.gameObject.SetActive(false))
                .AppendCallback(() => onComplete?.Invoke());
        }
    }
}
