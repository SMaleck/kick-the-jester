using Assets.Source.App;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class TitleMenuPanel : MonoBehaviour
    {
        [Header("Panel Properties")]
        [SerializeField] Button startButton;
        [SerializeField] AudioClip bgmTitle;
        [SerializeField] AudioClip bgmTransition;
        [SerializeField] float startDelaySeconds = 2f;


        private void Start()
        {
            startButton.OnClickAsObservable()
                       .Subscribe(_ => StartGame())
                       .AddTo(this);

            Kernel.AudioService.PlayLoopingBGM(bgmTitle);
        }

        private void StartGame()
        {
            startButton.interactable = false;
            Kernel.AudioService.PlayBGM(bgmTransition);

            Observable.Timer(TimeSpan.FromSeconds(startDelaySeconds))
                      .Subscribe(_ => Kernel.SceneTransitionService.ToGame())
                      .AddTo(this);
        }
    }
}
