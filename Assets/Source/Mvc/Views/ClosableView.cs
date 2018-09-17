using Assets.Source.Util.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class ClosableView : AbstractView
    {
        [Header("Closable Settings")]
        [SerializeField] private Button _closeButton;
        [SerializeField] private bool _startClosed = true;

        [Header("Transition")]
        [SerializeField] private PanelSliderConfig _panelSliderConfig;
        private PanelSlider _panelSlider;


        public override void Setup()
        {
            var ownerTransform = this.transform as RectTransform;
            var containerTransform = GetComponentInParent<Canvas>().transform as RectTransform;

            _panelSlider = new PanelSlider(ownerTransform, containerTransform.rect, _panelSliderConfig);

            _closeButton?.OnClickAsObservable()
                .Subscribe(_ => Close())
                .AddTo(this);

            if (_startClosed)
            {
                gameObject.SetActive(false);
                Close();
            }
        }


        public virtual void Open()
        {
            MessageBroker.Default.Publish(SlideEvent.Open);
            _panelSlider.SlideIn();
        }


        public virtual void Close()
        {
            MessageBroker.Default.Publish(SlideEvent.Close);
            _panelSlider.SlideOut();
        }
    }
}
