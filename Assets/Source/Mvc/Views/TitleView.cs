using Assets.Source.Services;
using Assets.Source.Util.UI;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class TitleView : AbstractView
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _startButtonText;

        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _tutorialButton;
        [SerializeField] private TextMeshProUGUI _tutorialButtonText;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private TextMeshProUGUI _creditsButtonText;
        [SerializeField] public AudioClip TransitionMusic;

        private readonly ReactiveCommand _onStartClicked = new ReactiveCommand();
        public IObservable<Unit> OnStartClicked => _onStartClicked;

        private readonly ReactiveCommand _onSettingsClicked = new ReactiveCommand();
        public IObservable<Unit> OnSettingsClicked => _onSettingsClicked;

        private readonly ReactiveCommand _onTutorialClicked = new ReactiveCommand();
        public IObservable<Unit> OnTutorialClicked => _onTutorialClicked;

        private readonly ReactiveCommand _onCreditsClicked = new ReactiveCommand();
        public IObservable<Unit> OnCreditsClicked => _onCreditsClicked;

        public override void Setup()
        {
            _onTutorialClicked.AddTo(Disposer);
            _onCreditsClicked.AddTo(Disposer);

            gameObject.SetActive(true);

            _onStartClicked.AddTo(Disposer);
            _startButton.OnClickAsObservable()
                .Subscribe(_ => OnStartClickedProxy())
                .AddTo(this);

            _onSettingsClicked.AddTo(Disposer);
            _onSettingsClicked.BindTo(_settingsButton).AddTo(Disposer);

            _onTutorialClicked.AddTo(Disposer);
            _onTutorialClicked.BindTo(_tutorialButton).AddTo(Disposer);

            _onCreditsClicked.AddTo(Disposer);
            _onCreditsClicked.BindTo(_creditsButton).AddTo(Disposer);

            UpdateTexts();
        }

        private void UpdateTexts()
        {
            _startButtonText.text = TextService.PlayExclamation();
            _tutorialButtonText.text = TextService.HowToPlay();
            _creditsButtonText.text = TextService.Credits();
        }

        private void OnStartClickedProxy()
        {
            _startButton.interactable = false;
            _startButton.GetComponent<Pulse>().Stop();

            _onStartClicked.Execute();
        }
    }
}
