using Assets.Source.Util;
using System;
using System.Collections.Generic;
using UniRx;

namespace Assets.Source.Mvc.Mediation
{
    public class ClosableViewMediator : AbstractDisposable, IClosableViewMediator, IClosableViewRegistrar
    {
        private readonly Dictionary<ClosableViewType, IClosableViewController> _closableViewControllers;

        private readonly Subject<ClosableViewType> _onViewOpened;
        public IObservable<ClosableViewType> OnViewOpened => _onViewOpened;

        private readonly Subject<ClosableViewType> _onViewClosed;
        public IObservable<ClosableViewType> OnViewClosed => _onViewClosed;

        public ClosableViewMediator()
        {
            _closableViewControllers = new Dictionary<ClosableViewType, IClosableViewController>();

            _onViewOpened = new Subject<ClosableViewType>().AddTo(Disposer);
            _onViewClosed = new Subject<ClosableViewType>().AddTo(Disposer);
        }

        void IClosableViewRegistrar.RegisterClosableView(
            ClosableViewType closableViewType,
            IClosableViewController closableViewController)
        {
            _closableViewControllers.Add(closableViewType, closableViewController);

            closableViewController.OnViewOpen
                .Subscribe(_ => _onViewOpened.OnNext(closableViewType))
                .AddTo(Disposer);

            closableViewController.OnViewClose
                .Subscribe(_ => _onViewClosed.OnNext(closableViewType))
                .AddTo(Disposer);
        }

        public void Open(ClosableViewType closableViewType)
        {
            if (_closableViewControllers.TryGetValue(closableViewType, out var controller))
            {
                controller.Open();
            }
        }

        public void Close(ClosableViewType closableViewType)
        {
            if (_closableViewControllers.TryGetValue(closableViewType, out var controller))
            {
                controller.Close();
            }
        }

        public bool IsViewOpen(ClosableViewType closableViewType)
        {
            if (_closableViewControllers.TryGetValue(closableViewType, out var controller))
            {
                return controller.IsOpen;
            }

            return false;
        }
    }
}
