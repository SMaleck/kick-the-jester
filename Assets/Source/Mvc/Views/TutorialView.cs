using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{    
    public class TutorialView : ClosableView
    {
        [SerializeField] private List<CanvasGroup> _steps;
        [SerializeField] private Button _nextButton;
        [SerializeField] private TMP_Text _instructionText;

        private const float InstructionPulseSeconds = 0.7f;
        private const int FontSizeMin = 48;
        private const int FontSizeMax = 52;
        private const float FadeTimeSeconds = 0.5f;        

        private FloatReactiveProperty fontSize = new FloatReactiveProperty(FontSizeMin);

        private int _currentStep = 0;
        private bool IsLastStep => _currentStep >= _steps.Count - 1;

        // ToDo DISPOSER
        public ReactiveCommand OnNextClickedOnLastSlide = new ReactiveCommand();


        public override void Setup()
        {
            base.Setup();

            ResetSlides();
            _steps.ForEach(step =>
            {
                step.gameObject.SetActive(true);
            });

            _nextButton
                .OnClickAsObservable()
                .Subscribe(_ => Next())
                .AddTo(this);

            // Animation for instruction text
            DOTween.To(() => fontSize.Value, (val) => fontSize.Value = val, FontSizeMax, InstructionPulseSeconds)
                .SetEase(Ease.InOutCubic)
                .SetLoops(-1, LoopType.Yoyo);

            fontSize
                .Subscribe(fontSize => { _instructionText.fontSize = fontSize; })
                .AddTo(this);
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


        public override void Open()
        {
            ResetSlides();
            base.Open();
        }


        private void Next()
        {
            // Close if this is the last step
            if (IsLastStep)
            {
                OnNextClickedOnLastSlide.Execute();
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
