using Assets.Source.Util.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{    
    // ToDo play music for clicked
    public class TitleView : AbstractView
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _tutorialButton;
        [SerializeField] private Button _creditsButton;

        [SerializeField] public AudioClip _TransitionMusic;

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
        }

        private void OnStartClickedProxy()
        {
            _startButton.interactable = false;
            _startButton.GetComponent<Pulse>().Stop();

            OnStartClicked.Execute();
        }
    }
}
