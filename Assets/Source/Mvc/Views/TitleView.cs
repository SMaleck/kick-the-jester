using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class TitleView : AbstractView
    {
        [SerializeField] public Button _startButton;
        [SerializeField] public Button _settingsButton;
        [SerializeField] public Button _tutorialButton;
        [SerializeField] public Button _creditsButton;

        public Action OnStartClicked = () => { };
        public Action OnSettingsClicked = () => { };
        public Action OnTutorialClicked = () => { };
        public Action OnCreditsClicked = () => { };


        public override void Setup()
        {
            gameObject.SetActive(true);

            _startButton.OnClickAsObservable()
                .Subscribe(_ => OnStartClicked())
                .AddTo(this);

            _settingsButton.OnClickAsObservable()
                .Subscribe(_ => OnSettingsClicked())
                .AddTo(this);

            _tutorialButton.OnClickAsObservable()
                .Subscribe(_ => OnTutorialClicked())
                .AddTo(this);

            _creditsButton.OnClickAsObservable()
                .Subscribe(_ => OnCreditsClicked())
                .AddTo(this);            
        }
    }
}
