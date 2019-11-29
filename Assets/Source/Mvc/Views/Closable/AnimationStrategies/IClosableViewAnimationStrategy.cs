using System;

namespace Assets.Source.Mvc.Views.Closable.AnimationStrategies
{
    public interface IIClosableViewAnimationStrategy
    {
        void Open(Action onComplete);
        void Close(Action onComplete);
    }
}
