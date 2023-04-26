using Assets.Source.Mvc.Views;
using Assets.Source.Mvc.Views.Closable;
using System;
using System.Collections;
using System.Globalization;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Features.Cheats
{
    public class CheatView : AbstractView
    {
        [Header("Opening")]
        [SerializeField] private GameObject _openButtonParent;
        [SerializeField] private Button _openButton;
        [SerializeField] private ClosableView _closableView;

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
            if (!Debug.isDebugBuild)
            {
                SetActive(false);
                _openButtonParent.SetActive(false);
                return;
            }

            _openButton.OnClickAsObservable()
                .Subscribe(_ => _closableView.Open())
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

        private void Update()
        {
            if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.F12))
            {
                StartCoroutine(nameof(TakeScreenshot));
            }
        }

        private IEnumerator TakeScreenshot()
        {
            _openButtonParent.SetActive(false);
            yield return new WaitForEndOfFrame();

            var now = DateTime.Now;
            var filename = $"screenshot_{now.ToString("yyyy-MM-dd-HHmmss")}.jpg";
            var filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);
            ScreenCapture.CaptureScreenshot(filePath);

            _openButtonParent.SetActive(true);
        }
    }
}
