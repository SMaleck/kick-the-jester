using Assets.Source.Util;
using System;
using UniRx;
using Zenject;

namespace Assets.Source.Mvc.Mediation
{
    public class ClosableViewController : AbstractDisposable, IClosableViewController
    {
        public class Factory : PlaceholderFactory<IClosableView, ClosableViewController> { }

        private readonly IClosableView _closableView;

        public bool IsOpen => _closableView.IsOpen;

        public IObservable<Unit> OnViewOpen => _closableView.OnViewOpen;
        public IObservable<Unit> OnViewOpenCompleted => _closableView.OnViewOpenCompleted;

        public IObservable<Unit> OnViewClose => _closableView.OnViewClose;
        public IObservable<Unit> OnViewCloseCompleted => _closableView.OnViewCloseCompleted;

        public ClosableViewController(IClosableView closableView)
        {
            _closableView = closableView;

            _closableView.OnCloseClicked
                .Subscribe(_ => Close())
                .AddTo(Disposer);
        }

        public void Open()
        {
            _closableView.Open();
        }

        public void Close()
        {
            _closableView.Close();
        }
    }
}