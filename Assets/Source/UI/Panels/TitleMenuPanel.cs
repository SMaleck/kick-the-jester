using Assets.Source.App;
using Assets.Source.UI.Elements;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class TitleMenuPanel : AbstractPanel
    {
        [Header("Panel Properties")]
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button startButton;

        [SerializeField] private AudioClip bgmTitle;
        [SerializeField] private AudioClip bgmTransition;
        [SerializeField] private float startDelaySeconds = 2f;


        public override void Setup()
        {
            base.Setup();

            startButton.OnClickAsObservable()
                       .Subscribe(_ => StartGame())
                       .AddTo(this);

            creditsButton.OnClickAsObservable()
                         .Subscribe(_ => ShowPanelByName("PF_Panel_Credits"))
                         .AddTo(this);

            Kernel.AudioService.PlayLoopingBGM(bgmTitle);
        }


        private void StartGame()
        {
            startButton.interactable = false;
            startButton.GetComponent<Pulse>().Stop();

            Kernel.AudioService.PlayBGM(bgmTransition);

            Observable.Timer(TimeSpan.FromSeconds(startDelaySeconds))
                      .Subscribe(_ => Kernel.SceneTransitionService.ToGame())
                      .AddTo(this);
        }
    }
}
