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

            // Listeners to User Settings
            App.Cache.RepoRx.UserSettingsRepository.MuteBGMProperty.Subscribe((bool value) => 
            {
                toggleBGMMuteButton.GetComponentInChildren<Text>().text = "BGM " + (value ? "(OFF)" : "(ON)");                
            });

            App.Cache.RepoRx.UserSettingsRepository.MuteSFXProperty.Subscribe((bool value) => 
            {
                toggleSFXMuteButton.GetComponentInChildren<Text>().text = "SFX " + (value ? "(OFF)" : "(ON)");
            });
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
            App.Cache.RepoRx.UserSettingsRepository.MuteBGM = !App.Cache.RepoRx.UserSettingsRepository.MuteBGM;
        }


        private void OnSFXMuteClicked()
        {
            App.Cache.RepoRx.UserSettingsRepository.MuteSFX = !App.Cache.RepoRx.UserSettingsRepository.MuteSFX;
        }        
    }
}
