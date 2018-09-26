using Assets.Source.App;
using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class TutorialPanel : AbstractPanel
    {
        private float fadeTimeSeconds = 0.5f;
        private int currentStep = 0;        

        private bool isLastStep
        {
            get
            {
                return currentStep >= steps.Count - 1;
            }
        }

        [SerializeField] private List<CanvasGroup> steps;
        [SerializeField] private Button nextButton;

        [SerializeField] private TextMeshProUGUI instructionText;
        [SerializeField] private float instructionPulseSeconds;
        [SerializeField] private int fontSizeMin = 48;
        [SerializeField] private int fontSizeMax = 52;


        private BoolReactiveProperty isOnFirstStep = new BoolReactiveProperty(true);
        private BoolReactiveProperty isOnLastStep = new BoolReactiveProperty(false);


        public override void Setup()
        {
            base.Setup();

            ResetSlides();

            nextButton.OnClickAsObservable().Subscribe(_ => Next());

            // Animation for instruction text
            LeanTween.value(this.gameObject, 
                            e => { instructionText.fontSize = (float)Math.Round((double)e, 1); },
                            fontSizeMin, fontSizeMax, instructionPulseSeconds)
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


        public override void Show()
        {
            ResetSlides();
            base.Show();
        }


        private void Next()
        {
            // Close if this is the last step
            if (isLastStep)
            {
                Close();
                return;
            }

            // Fade between current and next step
            FadeInStep(currentStep + 1);
            FadeOutStep(currentStep);

            currentStep++;
        }


        private void Close()
        {
            // Start the Game if this was the first start, because then this was shown automatically
            if (Kernel.PlayerProfile.Stats.IsFirstStart)
            {
                Kernel.PlayerProfile.Stats.IsFirstStart = false;
                Kernel.SceneTransitionService.ToGame();
                return;
            }

            Hide();
        }


        private void FadeInStep(int index, float time = -1)
        {
            time = time <= -1 ? fadeTimeSeconds : time;
            LeanTween.value(this.gameObject, (float value) => { steps[index].alpha = value; }, 0, 1, time);
        }


        private void FadeOutStep(int index, float time = -1)
        {
            time = time <= -1 ? fadeTimeSeconds : time;
            LeanTween.value(this.gameObject, (float value) => { steps[index].alpha = value; }, 1, 0, time);
        }
    }
}
