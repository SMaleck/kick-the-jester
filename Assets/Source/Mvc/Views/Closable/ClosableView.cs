using Assets.Source.Mvc.Mediation;
using Assets.Source.Mvc.Views.Closable.AnimationStrategies;
using Assets.Source.Util;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Source.Mvc.Views.Closable
{
    public class ClosableView : AbstractDisposableMonoBehaviour, IClosableView, IInitializable
    {
        [Tooltip("Leave empty to use this gameObject's transform")]
        [SerializeField] private GameObject _closableParent;
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private Button _closeButton;
        [SerializeField] private bool _startClosed = true;

        [SerializeField] private ClosableVIewAnimationType _animationType;

        private IIClosableViewAnimationStrategy _animationStrategy;

        public bool IsOpen => _closableParent.gameObject.activeSelf;

        private Subject<Unit> _onViewOpen;
        public IObservable<Unit> OnViewOpen => _onViewOpen;
        
        private Subject<Unit> _onViewOpenCompleted;
        public IObservable<Unit> OnViewOpenCompleted => _onViewOpenCompleted;

        private Subject<Unit> _onViewClose;
        public IObservable<Unit> OnViewClose => _onViewClose;

        private Subject<Unit> _onViewCloseCompleted;
        public IObservable<Unit> OnViewCloseCompleted => _onViewCloseCompleted;


        private readonly ReactiveCommand _onCloseClicked = new ReactiveCommand();
        public IObservable<Unit> OnCloseClicked => _onCloseClicked;

        public void Initialize()
        {
            _closableParent = _closableParent ?? gameObject;
            _animationStrategy = CreateAnimationStrategy();

            _onViewOpen = new Subject<Unit>().AddTo(Disposer);
            _onViewOpenCompleted = new Subject<Unit>().AddTo(Disposer);
            _onViewClose = new Subject<Unit>().AddTo(Disposer);
            _onViewCloseCompleted = new Subject<Unit>().AddTo(Disposer);

            if (_closeButton != null)
            {
                _onCloseClicked.AddTo(Disposer);
                _onCloseClicked.BindTo(_closeButton).AddTo(Disposer);
            }

            _closableParent.SetActive(!_startClosed);
        }

        private IIClosableViewAnimationStrategy CreateAnimationStrategy()
        {
            switch (_animationType)
            {
                case ClosableVIewAnimationType.None:
                    return AnimationStrategyFactory.CreateDefaultAnimationStrategy(
                        _closableParent);

                case ClosableVIewAnimationType.PopOut:
                    return AnimationStrategyFactory.CreatePopOutAnimationStrategy(
                        _closableParent.transform);

                case ClosableVIewAnimationType.Fade:
                    return AnimationStrategyFactory.CreateFadeAnimationStrategy(
                        _closableParent.transform,
                        _canvasGroup);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Open()
        {
            _onViewOpen.OnNext(Unit.Default);

            _animationStrategy.Open(
                () => _onViewOpenCompleted.OnNext(Unit.Default));
        }

        public void Close()
        {
            _onViewClose.OnNext(Unit.Default);

            _animationStrategy.Close(
                () => _onViewCloseCompleted.OnNext(Unit.Default));
        }
    }
}
