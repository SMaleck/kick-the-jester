using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class ShopConfirmResetView : ClosableView
    {
        [SerializeField] private Button _confirmButton;

        public ReactiveCommand OnResetConfirmClicked = new ReactiveCommand();

        public override void Setup()
        {
            base.Setup();

            _confirmButton.OnClickAsObservable()
                .Subscribe(_ => OnResetConfirmClicked.Execute())
                .AddTo(this);
        }
    }
}
