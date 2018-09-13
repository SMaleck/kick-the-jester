using Assets.Source.Util.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class ClosableView : AbstractView
    {        
        [SerializeField] private Button _closeButton;
        [SerializeField] private bool _startClosed = true;

        [Header("Transition")]
        [SerializeField] private PanelSliderConfig _panelSliderConfig;
        private PanelSlider _panelSlider;


        public override void Initialize()
        {
            var ownerTransform = this.transform as RectTransform;
            var containerTransform = GetComponentInParent<Canvas>().transform as RectTransform;

            _panelSlider = new PanelSlider(ownerTransform, containerTransform.rect, _panelSliderConfig);

            _closeButton?.OnClickAsObservable()
                .Subscribe(_ => Close())
                .AddTo(this);

            if (_startClosed)
            {
                Close();
            }
        }


        public virtual void Open()
        {
            _panelSlider.SlideIn();
        }


        public virtual void Close()
        {
            _panelSlider.SlideOut();
        }
    }
}
