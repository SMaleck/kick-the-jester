using Assets.Source.Mvc.Mediation;
using Assets.Source.Util;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Source.Mvc.Views.Components
{
    public class ClosableView : AbstractDisposableMonoBehaviour, IClosableView, IInitializable
    {
        [Tooltip("Leave empty to use this gameObject's transform")]
        [SerializeField] private GameObject _closableParent;

        [SerializeField] private Button _closeButton;
        [SerializeField] private bool _startClosed = true;

        private Subject<Unit> _onOpen;
        public IObservable<Unit> OnOpen => _onOpen;

        private Subject<Unit> _onClose;
        public IObservable<Unit> OnClose => _onClose;

        public void Initialize()
        {
            _closableParent = _closableParent ?? gameObject;

            _onOpen = new Subject<Unit>().AddTo(Disposer);
            _onClose = new Subject<Unit>().AddTo(Disposer);

            _closeButton?.OnClickAsObservable()
                .Subscribe(_ => Close())
                .AddTo(Disposer);

            if (_startClosed)
            {
                _closableParent.SetActive(false);
            }
        }

        public void Open()
        {
            _closableParent.SetActive(true);
            _onOpen.OnNext(Unit.Default);
        }

        public void Close()
        {
            _closableParent.SetActive(false);
            _onOpen.OnNext(Unit.Default);
        }
    }
}
