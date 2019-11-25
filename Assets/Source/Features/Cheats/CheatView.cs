using Assets.Source.Mvc.Views;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Source.Features.Cheats
{
    public class CheatView : ClosableView
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, CheatView> { }

        [Header("Opening")]
        [SerializeField] private GameObject _openButtonParent;
        [SerializeField] private Button _openButton;

        [Header("Cheat Buttons")]
        [SerializeField] private Button _giveMuchCurrencyButton;
        [SerializeField] private Button _slowTimeButton;
        [SerializeField] private Button _addPickUpButton;
        [SerializeField] private Button _addProjectileButton;
        [SerializeField] private Button _switchLanguageButton;

        private readonly ReactiveCommand _onGiveMuchCurrencyClicked = new ReactiveCommand();
        public IObservable<Unit> OnGiveMuchCurrencyClicked => _onGiveMuchCurrencyClicked;

        private readonly ReactiveCommand _onSlowTimeClicked = new ReactiveCommand();
        public IObservable<Unit> OnSlowTimeClicked => _onSlowTimeClicked;

        private readonly ReactiveCommand _onAddPickupClicked = new ReactiveCommand();
        public IObservable<Unit> OnAddPickupClicked => _onAddPickupClicked;

        private readonly ReactiveCommand _onAddProjectileClicked = new ReactiveCommand();
        public IObservable<Unit> OnAddProjectileClicked => _onAddProjectileClicked;

        private readonly ReactiveCommand _onSwitchLanguageClicked = new ReactiveCommand();
        public IObservable<Unit> OnSwitchLanguageClicked => _onSwitchLanguageClicked;


        public override void Setup()
        {
            base.Setup();

            if (!Debug.isDebugBuild)
            {
                SetActive(false);
                _openButtonParent.SetActive(false);
                return;
            }

            _openButton.OnClickAsObservable()
                .Subscribe(_ => Open())
                .AddTo(Disposer);

            _onGiveMuchCurrencyClicked.AddTo(Disposer);
            _onGiveMuchCurrencyClicked.BindTo(_giveMuchCurrencyButton).AddTo(Disposer);

            _onSlowTimeClicked.AddTo(Disposer);
            _onSlowTimeClicked.BindTo(_slowTimeButton).AddTo(Disposer);

            _onAddPickupClicked.AddTo(Disposer);
            _onAddPickupClicked.BindTo(_addPickUpButton).AddTo(Disposer);

            _onAddProjectileClicked.AddTo(Disposer);
            _onAddProjectileClicked.BindTo(_addProjectileButton).AddTo(Disposer);

            _onSwitchLanguageClicked.AddTo(Disposer);
            _onSwitchLanguageClicked.BindTo(_switchLanguageButton).AddTo(Disposer);
        }
    }
}
