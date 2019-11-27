using System;
using UniRx;

namespace Assets.Source.Mvc.Mediation
{
    public interface IClosableView
    {
        void Open();
        void Close();

        bool IsOpen { get; }

        IObservable<Unit> OnViewOpened { get; }
        IObservable<Unit> OnViewClosed { get; }
        IObservable<Unit> OnCloseClicked { get; }
    }
}
