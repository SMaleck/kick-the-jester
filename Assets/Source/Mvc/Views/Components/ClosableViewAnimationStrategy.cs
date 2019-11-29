using Assets.Source.Util;
using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Source.Mvc.Views.Components
{
    public class ClosableViewAnimationStrategy : AbstractDisposable
    {
        public static class Factory
        {
            public static ClosableViewAnimationStrategy Create(Transform target)
            {
                return new ClosableViewAnimationStrategy(target);
            }
        }

        private const float TransitionTimeSeconds = 0.2f;

        private readonly Transform _target;
        private readonly Vector3 _originalScale;

        public ClosableViewAnimationStrategy(Transform target)
        {
            _target = target;
            _originalScale = _target.localScale;
        }

        public void Open(Action onComplete)
        {
            _target.gameObject.SetActive(true);
            _target.localScale = Vector3.zero;

            DOTween.Sequence()
                .Append(_target.DOScale(_originalScale, TransitionTimeSeconds))
                .AppendCallback(() => onComplete?.Invoke());
        }

        public void Close(Action onComplete)
        {
            DOTween.Sequence()
                .Append(_target.DOScale(Vector3.zero, TransitionTimeSeconds))
                .AppendCallback(() => _target.gameObject.SetActive(false))
                .AppendCallback(() => onComplete?.Invoke());
        }
    }
}
