using System;
using UnityEngine;

namespace Assets.Source.Mvc.Views.Closable.AnimationStrategies
{
    public class DefaultAnimationStrategy : IClosableViewAnimationStrategy
    {
        private readonly GameObject _target;

        public DefaultAnimationStrategy(GameObject target)
        {
            _target = target;
        }

        public void Initialize(bool startClosed)
        {
            _target.SetActive(!startClosed);
        }

        public void Open(Action onComplete)
        {
            _target.gameObject.SetActive(true);
            onComplete?.Invoke();
        }

        public void Close(Action onComplete)
        {
            _target.gameObject.SetActive(false);
            onComplete?.Invoke();
        }
    }
}
