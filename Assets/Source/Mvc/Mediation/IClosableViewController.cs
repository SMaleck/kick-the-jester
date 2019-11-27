using System;
using UniRx;

namespace Assets.Source.Mvc.Mediation
{
    public interface IClosableViewController
    {
        void Open();
        void Close();

        bool IsOpen { get; }

        IObservable<Unit> OnViewOpened { get; }
        IObservable<Unit> OnViewClosed { get; }
    }
}
