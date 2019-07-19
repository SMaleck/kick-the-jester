using Assets.Source.Services;
using Assets.Source.Util.UI;
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

        // ToDo Try to get rid of this
        [SerializeField] public AudioClip TransitionMusic;

        // ToDo DISPOSER
        public ReactiveCommand OnStartClicked = new ReactiveCommand();
        public ReactiveCommand OnSettingsClicked = new ReactiveCommand();
        public ReactiveCommand OnTutorialClicked = new ReactiveCommand();
        public ReactiveCommand OnCreditsClicked = new ReactiveCommand();

        public override void Setup()
        {
            gameObject.SetActive(true);

            _startButton.OnClickAsObservable()
                .Subscribe(_ => OnStartClickedProxy())
                .AddTo(this);

            _settingsButton.OnClickAsObservable()
                .Subscribe(_ => OnSettingsClicked.Execute())
                .AddTo(this);

            _tutorialButton.OnClickAsObservable()
                .Subscribe(_ => OnTutorialClicked.Execute())
                .AddTo(this);

            _creditsButton.OnClickAsObservable()
                .Subscribe(_ => OnCreditsClicked.Execute())
                .AddTo(this);

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

            OnStartClicked.Execute();
        }
    }
}
