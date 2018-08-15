using Assets.Source.App;
using Assets.Source.App.Audio;
using Assets.Source.UI.Elements;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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

        // TODO: Find out how to have this installed in the container to enable dependency injection to work
        private AudioService audioService;
        [Inject]
        public void Init(AudioService audioService)
        {
            this.audioService = audioService;
        }

        public override void Setup()
        {
            base.Setup();
            LateSetup();
        }
        
        private void LateSetup()
        {
            startButton.OnClickAsObservable()
                       .Subscribe(_ => StartGame())
                       .AddTo(this);

            creditsButton.OnClickAsObservable()
                         .Subscribe(_ => ShowPanelByName("PF_Panel_Credits"))
                         .AddTo(this);

            // Deactivate tutorial button on first start, because we will show it automatically
            tutorialButton.gameObject.SetActive(!App.Cache.Kernel.PlayerProfile.Stats.IsFirstStart);
            tutorialButton.OnClickAsObservable()
                         .Subscribe(_ => ShowPanelByName("PF_Panel_Tutorial"))
                         .AddTo(this);

            audioService.PlayLoopingBGM(bgmTitle);
        }


        private void StartGame()
        {
            startButton.interactable = false;
            startButton.GetComponent<Pulse>().Stop();

            App.Cache.Kernel.AudioService.PlayBGM(bgmTransition);

            // If this is the first start, then show the tutorial
            if (App.Cache.Kernel.PlayerProfile.Stats.IsFirstStart)
            {
                ShowPanelByName("PF_Panel_Tutorial");
                return;
            }

            // Stat Game after delay
            Observable.Timer(TimeSpan.FromSeconds(startDelaySeconds))
                      .Subscribe(_ => App.Cache.Kernel.SceneTransitionService.ToGame())
                      .AddTo(this);
        }
    }
}
