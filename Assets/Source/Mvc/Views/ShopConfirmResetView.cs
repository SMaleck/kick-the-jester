using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class ShopConfirmResetView : ClosableView
    {
        [SerializeField] private Button _confirmButton;

        private readonly ReactiveCommand _onResetConfirmClicked = new ReactiveCommand();
        public IObservable<Unit> OnResetConfirmClicked => _onResetConfirmClicked;

        public override void Setup()
        {
            base.Setup();

            _onResetConfirmClicked.AddTo(Disposer);
            _onResetConfirmClicked.BindTo(_confirmButton).AddTo(Disposer);
        }
    }
}
