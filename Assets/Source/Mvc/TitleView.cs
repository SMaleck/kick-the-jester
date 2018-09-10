using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc
{
    public class TitleView : AbstractView
    {
        [SerializeField] public Button _startButton;
        [SerializeField] public Button _settingsButton;

        public Action OnStartClicked = () => { };
        public Action OnSettingsClicked = () => { };

        public override void Initialize()
        {
            _startButton.OnClickAsObservable()
                .Subscribe(_ => OnStartClicked())
                .AddTo(this);

            _settingsButton.OnClickAsObservable()
                .Subscribe(_ => OnSettingsClicked())
                .AddTo(this);
        }
    }
}
