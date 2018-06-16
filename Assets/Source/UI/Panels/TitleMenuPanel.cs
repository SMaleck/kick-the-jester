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
        [SerializeField] private Button tutorialButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button startButton;

        [SerializeField] private AudioClip bgmTitle;
        [SerializeField] private AudioClip bgmTransition;
        [SerializeField] private float startDelaySeconds = 1.5f;


        public override void Setup()
        {
            base.Setup();

            startButton.OnClickAsObservable()
                       .Subscribe(_ => StartGame())
                       .AddTo(this);

            creditsButton.OnClickAsObservable()
                         .Subscribe(_ => ShowPanelByName("PF_Panel_Credits"))
                         .AddTo(this);

            // Deactivate tutorial button on first start, because we will show it automatically
            tutorialButton.gameObject.SetActive(!Kernel.PlayerProfileService.IsFirstStart);
            tutorialButton.OnClickAsObservable()
                         .Subscribe(_ => ShowPanelByName("PF_Panel_Tutorial"))
                         .AddTo(this);

            Kernel.AudioService.PlayLoopingBGM(bgmTitle);
        }


        private void StartGame()
        {
            startButton.interactable = false;
            startButton.GetComponent<Pulse>().Stop();

            Kernel.AudioService.PlayBGM(bgmTransition);

            // If this is the first start, then show the tutorial
            if (Kernel.PlayerProfileService.IsFirstStart)
            {
                ShowPanelByName("PF_Panel_Tutorial");
                return;
            }

            // Stat Game after delay
            Observable.Timer(TimeSpan.FromSeconds(startDelaySeconds))
                      .Subscribe(_ => Kernel.SceneTransitionService.ToGame())
                      .AddTo(this);
        }
    }
}
