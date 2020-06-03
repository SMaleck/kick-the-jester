using System;

namespace Assets.Source.Mvc.Views.Closable.AnimationStrategies
{
    public interface IClosableViewAnimationStrategy
    {
        void Initialize(bool startClosed);
        void Open(Action onComplete);
        void Close(Action onComplete);
    }
}
