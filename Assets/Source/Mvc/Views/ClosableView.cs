using Assets.Source.Util.UI;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class ClosableView : AbstractView
    {
        [Header("Closable Settings")]
        [SerializeField] private bool _startClosed = true;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Vector3 _closedPosition;
        [Tooltip("Leave empty to use this gameObject's transform")]
        [SerializeField] private RectTransform _panelParent;

        [Header("Transition")]
        [SerializeField] private PanelSliderConfig _panelSliderConfig;

        private PanelSlider _panelSlider;

        private RectTransform PanelParent => _panelParent == null ? (transform as RectTransform) : _panelParent;

        public bool IsOpen => PanelParent.gameObject.activeSelf;

        private readonly ReactiveCommand _onOpenCompleted = new ReactiveCommand();
        public IObservable<Unit> OnOpenCompleted => _onOpenCompleted;

        private readonly ReactiveCommand _onCloseCompleted = new ReactiveCommand();
        public IObservable<Unit> OnCloseCompleted => _onCloseCompleted;


        public override void Setup()
        {
            _onOpenCompleted.AddTo(Disposer);
            _onCloseCompleted.AddTo(Disposer);

            _panelSlider = new PanelSlider(
                    PanelParent,
                    _panelSliderConfig,
                    PanelParent.localPosition,
                    _closedPosition)
                .AddTo(Disposer);

            _panelSlider.OnOpenCompleted
                .Subscribe(_ => _onOpenCompleted.Execute())
                .AddTo(Disposer);

            _panelSlider.OnCloseCompleted
                .Subscribe(_ => _onCloseCompleted.Execute())
                .AddTo(Disposer);

            _closeButton?.OnClickAsObservable()
                .Subscribe(_ => Close())
                .AddTo(Disposer);

            if (_startClosed)
            {
                _panelSlider.SetClosed();
            }
            else
            {
                _panelSlider.SetOpen();
            }
        }


        public virtual void Open()
        {
            _panelSlider.SlideOpen();
        }


        public virtual void Close()
        {
            _panelSlider.SlideClosed();
        }
    }
}
