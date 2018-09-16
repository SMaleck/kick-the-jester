using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    // ToDo Replace LeanTWeen with DOTween
    public class TutorialView : ClosableView
    {
        [SerializeField] private List<CanvasGroup> steps;
        [SerializeField] private Button nextButton;
        [SerializeField] private TMP_Text instructionText;

        private const float InstructionPulseSeconds = 0.7f;
        private const int FontSizeMin = 48;
        private const int FontSizeMax = 52;
        private const float FadeTimeSeconds = 0.5f;

        private int currentStep = 0;

        private bool isLastStep
        {
            get
            {
                return currentStep >= steps.Count - 1;
            }
        }

        public ReactiveCommand OnNextClickedOnLastSlide = new ReactiveCommand();


        public override void Initialize()
        {
            base.Initialize();

            ResetSlides();

            nextButton
                .OnClickAsObservable()
                .Subscribe(_ => Next())
                .AddTo(this);

            // Animation for instruction text
            LeanTween.value(this.gameObject,
                            e => { instructionText.fontSize = (float)Math.Round((double)e, 1); },
                            FontSizeMin, FontSizeMax, InstructionPulseSeconds)
                            .setLoopPingPong()
                            .setEaseInOutCubic();
        }


        private void ResetSlides()
        {
            currentStep = 0;

            // Hide all panels, except the first
            for (int i = 1; i < steps.Count; i++)
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
            if (isLastStep)
            {
                OnNextClickedOnLastSlide.Execute();
                return;
            }

            // Fade between current and next step
            FadeInStep(currentStep + 1);
            FadeOutStep(currentStep);

            currentStep++;
        }        


        private void FadeInStep(int index, float time = -1)
        {
            time = time <= -1 ? FadeTimeSeconds : time;
            LeanTween.value(this.gameObject, (float value) => { steps[index].alpha = value; }, 0, 1, time);
        }


        private void FadeOutStep(int index, float time = -1)
        {
            time = time <= -1 ? FadeTimeSeconds : time;
            LeanTween.value(this.gameObject, (float value) => { steps[index].alpha = value; }, 1, 0, time);
        }
    }
}
