using Assets.Source.Repositories;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] Button resumeButton;
        [SerializeField] Button retryButton;        

        [SerializeField] Button toggleSFXMuteButton;
        [SerializeField] Button toggleBGMMuteButton;

        private void Awake()
        {
            App.Cache.RepoRx.GameStateRepository.StateProperty
                                                .TakeUntilDestroy(this)                                                
                                                .Subscribe(TooglePanel);

            resumeButton.OnClickAsObservable().Subscribe(_ => OnResumeClicked());
            //retryButton.OnClickAsObservable().Subscribe(_ => OnRetryClicked());

            toggleSFXMuteButton.OnClickAsObservable().Subscribe(_ => OnSFXMuteClicked());
            toggleBGMMuteButton.OnClickAsObservable().Subscribe(_ => OnBGMMuteClicked());
        }


        private void TooglePanel(GameState state)
        {
            gameObject.SetActive(state == GameState.Paused);
        }


        private void OnResumeClicked()
        {
            App.Cache.userControl.TooglePause();
        }


        private void OnRetryClicked()
        {
            App.Cache.Services.SceneTransitionService.ToGame();
        }


        private void OnBGMMuteClicked()
        {
            App.Cache.Services.AudioService.ToggleBGMMuted();
            bool isMuted = App.Cache.Services.AudioService.IsBGMChannelMuted;

            toggleBGMMuteButton.GetComponentInChildren<Text>().text = "BGM " + (isMuted ? "(OFF)" : "(ON)");
        }


        private void OnSFXMuteClicked()
        {
            App.Cache.Services.AudioService.ToggleSFXMuted();
            bool isMuted = App.Cache.Services.AudioService.IsSFXChannelMuted;

            toggleSFXMuteButton.GetComponentInChildren<Text>().text = "SFX " + (isMuted ? "(OFF)" : "(ON)");
        }        
    }
}
