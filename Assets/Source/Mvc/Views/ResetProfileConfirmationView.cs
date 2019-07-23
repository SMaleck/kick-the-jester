using Assets.Source.Services;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class ResetProfileConfirmationView : ClosableView
    {
        [SerializeField] private TextMeshProUGUI _resetProfileTitleText;
        [SerializeField] private TextMeshProUGUI _explanationText;
        [SerializeField] private TextMeshProUGUI _warningText;
        [SerializeField] private TextMeshProUGUI _cancelButtonText;

        [SerializeField] private Button _confirmButton;
        [SerializeField] private TextMeshProUGUI _confirmButtonText;

        private readonly ReactiveCommand _onResetConfirmClicked = new ReactiveCommand();
        public IObservable<Unit> OnResetConfirmClicked => _onResetConfirmClicked;

        public override void Setup()
        {
            base.Setup();

            _onResetConfirmClicked.AddTo(Disposer);
            _onResetConfirmClicked.BindTo(_confirmButton).AddTo(Disposer);
        }

        private void UpdateTexts()
        {
            _resetProfileTitleText.text = TextService.ResetProfile();
            _explanationText.text = TextService.ResetProfileDescription();
            _warningText.text = TextService.ResetProfileWarning();
            _cancelButtonText.text = TextService.Cancel();
            _confirmButtonText.text = TextService.ResetProfile();
        }
    }
}
