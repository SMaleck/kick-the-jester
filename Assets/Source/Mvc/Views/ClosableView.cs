using Assets.Source.Util.UI;
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

        [Header("Transition")]
        [SerializeField] private PanelSliderConfig _panelSliderConfig;

        private PanelSlider _panelSlider;

        public bool IsOpen => gameObject.activeSelf;
        public ReactiveCommand OnOpenCompleted = new ReactiveCommand();
        public ReactiveCommand OnCloseCompleted = new ReactiveCommand();


        public override void Setup()
        {
            var ownerTransform = this.transform as RectTransform;            

            _panelSlider = new PanelSlider(ownerTransform, _panelSliderConfig, ownerTransform.localPosition, _closedPosition);

            _panelSlider.OnOpenCompleted.Subscribe(_ => OnOpenCompleted.Execute());
            _panelSlider.OnCloseCompleted.Subscribe(_ => OnCloseCompleted.Execute());

            _closeButton?.OnClickAsObservable()
                .Subscribe(_ => Close())
                .AddTo(this);

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
