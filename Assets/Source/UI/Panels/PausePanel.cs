using Assets.Source.Repositories;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] Button resumeButton;
        [SerializeField] Button toggleSFXMuteButton;
        [SerializeField] Button toggleBGMMuteButton;

        private void Awake()
        {
            App.Cache.RepoRx.GameStateRepository.StateProperty
                                                .TakeUntilDestroy(this)                                                
                                                .Subscribe(TooglePanel);

            resumeButton.OnClickAsObservable().Subscribe(_ => OnResumeClicked());            
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


        private void OnBGMMuteClicked()
        {
            App.Cache.audioService.ToggleBGMMuted();
            bool isMuted = App.Cache.audioService.IsBGMChannelMuted;

            toggleBGMMuteButton.GetComponentInChildren<Text>().text = "BGM " + (isMuted ? "(OFF)" : "(ON)");
        }


        private void OnSFXMuteClicked()
        {
            App.Cache.audioService.ToggleSFXMuted();
            bool isMuted = App.Cache.audioService.IsSFXChannelMuted;

            toggleSFXMuteButton.GetComponentInChildren<Text>().text = "SFX " + (isMuted ? "(OFF)" : "(ON)");
        }        
    }
}
