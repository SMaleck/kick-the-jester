using Assets.Source.Mvc.Views.Closable;
using Assets.Source.Services.Localization;
using Assets.Source.Util;
using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class TutorialView : AbstractView
    {
        [SerializeField] private ClosableView _closableView;

        [SerializeField] private List<CanvasGroup> _steps;
        [SerializeField] private Button _nextButton;
        [SerializeField] private TextMeshProUGUI _instructionText;
        [SerializeField] private TextMeshProUGUI _tutorialStepOneText;
        [SerializeField] private TextMeshProUGUI _tutorialStepTwoText;
        [SerializeField] private TextMeshProUGUI _tutorialStepThreeText;
        [SerializeField] private TextMeshProUGUI _tutorialStepThreeTipText;

        private const float InstructionPulseScale = 1.1f;
        private const float InstructionPulseSeconds = 0.7f;
        private const float FadeTimeSeconds = 0.5f;

        private int _currentStep = 0;
        private bool IsLastStep => _currentStep >= _steps.Count - 1;

        private readonly ReactiveCommand _onNextClickedOnLastSlide = new ReactiveCommand();
        public IObservable<Unit> OnNextClickedOnLastSlide => _onNextClickedOnLastSlide;

        public override void Setup()
        {
            _onNextClickedOnLastSlide.AddTo(Disposer);

            ResetSlides();
            _steps.ForEach(step =>
            {
                step.gameObject.SetActive(true);
            });

            _nextButton
                .OnClickAsObservable()
                .Subscribe(_ => Next())
                .AddTo(Disposer);

            _instructionText.transform
                .DOScale(InstructionPulseScale, InstructionPulseSeconds)
                .SetEase(Ease.InOutCubic)
                .SetLoops(-1, LoopType.Yoyo)
                .AddTo(Disposer, TweenDisposalBehaviour.Rewind);

            UpdateTexts();

            _closableView.OnViewOpen
                .Subscribe(_ => ResetSlides())
                .AddTo(Disposer);
        }

        private void UpdateTexts()
        {
            _instructionText.text = TextService.TapAnywhereToContinue();
            _tutorialStepOneText.text = TextService.TutorialStepOne();
            _tutorialStepTwoText.text = TextService.TutorialStepTwo();
            _tutorialStepThreeText.text = TextService.TutorialStepThree();
            _tutorialStepThreeTipText.text = TextService.TutorialStepThreeTip();
        }

        private void ResetSlides()
        {
            _currentStep = 0;

            // Hide all panels, except the first
            for (int i = 1; i < _steps.Count; i++)
            {
                FadeOutStep(i, 0);
            }

            FadeInStep(0, 0);
        }

        private void Next()
        {
            // Close if this is the last step
            if (IsLastStep)
            {
                _onNextClickedOnLastSlide.Execute();
                return;
            }

            // Fade between current and next step
            FadeInStep(_currentStep + 1);
            FadeOutStep(_currentStep);

            _currentStep++;
        }

        private void FadeInStep(int index, float time = -1)
        {
            time = time <= -1 ? FadeTimeSeconds : time;
            _steps[index].DOFade(1, time);
        }

        private void FadeOutStep(int index, float time = -1)
        {
            time = time <= -1 ? FadeTimeSeconds : time;
            _steps[index].DOFade(0, time);
        }
    }
}
