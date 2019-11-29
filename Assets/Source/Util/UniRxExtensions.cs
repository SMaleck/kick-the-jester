using System;
using System.Linq;
using UniRx;

namespace Assets.Source.Util
{
    public static class UniRxExtensions
    {
        public static IObservable<T> OncePerFrame<T>(this IObservable<T> observable)
        {
            return observable.BatchFrame().Select(batch => batch.Last());
        }
    }
}
