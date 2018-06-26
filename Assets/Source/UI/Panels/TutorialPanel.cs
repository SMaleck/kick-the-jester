using Assets.Source.App;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class TutorialPanel : AbstractPanel
    {
        private float fadeTimeSeconds = 0.5f;
        private int _currentStep = 0;
        private int currentStep
        {
            get { return _currentStep; }
            set
            {
                _currentStep = value;
                isOnFirstStep.Value = _currentStep <= 0;
                isOnLastStep.Value = _currentStep >= steps.Count - 1;
            }
        }

        [SerializeField] private List<CanvasGroup> steps;
        [SerializeField] private Button prevButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button closeButton;

        private BoolReactiveProperty isOnFirstStep = new BoolReactiveProperty(true);
        private BoolReactiveProperty isOnLastStep = new BoolReactiveProperty(false);


        public override void Setup()
        {
            base.Setup();

            ResetSlides();

            // Hide prev and close buttons initially
            prevButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);

            prevButton.OnClickAsObservable().Subscribe(_ => Prev());
            nextButton.OnClickAsObservable().Subscribe(_ => Next());
            closeButton.OnClickAsObservable().Subscribe(_ => Close());

            // Toggle button active state based on the step we are on
            isOnFirstStep.Subscribe((bool value) =>
            {
                prevButton.gameObject.SetActive(!value);                
            })
            .AddTo(this);

            isOnLastStep.Subscribe((bool value) =>
            {
                nextButton.gameObject.SetActive(!value);
                closeButton.gameObject.SetActive(value);
            })
            .AddTo(this);
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


        private void Prev()
        {            
            // Fade between current and previous step
            FadeInStep(currentStep - 1);
            FadeOutStep(currentStep);

            currentStep--;
        }


        private void Next()
        {
            // Fade between current and next step
            FadeInStep(currentStep + 1);
            FadeOutStep(currentStep);

            currentStep++;
        }


        private void Close()
        {
            // Start the Game if this was the first start, because then this was shown automatically
            if (App.Cache.Kernel.PlayerProfile.Stats.IsFirstStart)
            {
                App.Cache.Kernel.PlayerProfile.Stats.IsFirstStart = false;
                App.Cache.Kernel.SceneTransitionService.ToGame();
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
