using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc
{
    public class ClosableView : AbstractView
    {        
        [SerializeField] private Button _closeButton;
        [SerializeField] private bool _startClosed = true;

        protected override void Start()
        {
            _closeButton?.OnClickAsObservable()
                .Subscribe(_ => Close())
                .AddTo(this);

            if (_startClosed)
            {
                Close();
            }

            Initialize();
        }

        public override void Initialize()
        {
            _closeButton?.OnClickAsObservable()
                .Subscribe(_ => Close())
                .AddTo(this);
        }


        public virtual void Open()
        {
            this.gameObject.SetActive(true);
        }


        public virtual void Close()
        {
            this.gameObject.SetActive(false);
        }
    }
}
