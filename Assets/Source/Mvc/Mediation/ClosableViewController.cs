using System;
using Assets.Source.Util;
using UniRx;
using Zenject;

namespace Assets.Source.Mvc.Mediation
{
    public class ClosableViewController : AbstractDisposable, IClosableViewController
    {
        public class Factory : PlaceholderFactory<IClosableView, ClosableViewController> { }

        private readonly IClosableView _closableView;

        public bool IsOpen => _closableView.IsOpen;

        private readonly Subject<Unit> _onViewOpened;
        public IObservable<Unit> OnViewOpened => _onViewOpened;

        private readonly Subject<Unit> _onViewClosed;
        public IObservable<Unit> OnViewClosed => _onViewClosed;

        public ClosableViewController(IClosableView closableView)
        {
            _closableView = closableView;

            _onViewOpened = new Subject<Unit>().AddTo(Disposer);
            _onViewClosed = new Subject<Unit>().AddTo(Disposer);

            _closableView.OnCloseClicked
                .Subscribe(_ => Close())
                .AddTo(Disposer);
        }

        public void Open()
        {
            _closableView.Open();
            _onViewOpened.OnNext(Unit.Default);
        }

        public void Close()
        {
            _closableView.Close();
            _onViewClosed.OnNext(Unit.Default);
        }
    }
}
