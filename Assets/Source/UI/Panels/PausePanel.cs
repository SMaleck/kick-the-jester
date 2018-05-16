using Assets.Source.AppKernel;
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
            Kernel.UserSettingsService.MuteBGMProperty.Subscribe((bool value) => 
            {
                toggleBGMMuteButton.GetComponentInChildren<Text>().text = "BGM " + (value ? "(OFF)" : "(ON)");                
            }).AddTo(this);

            Kernel.UserSettingsService.MuteSFXProperty.Subscribe((bool value) => 
            {
                toggleSFXMuteButton.GetComponentInChildren<Text>().text = "SFX " + (value ? "(OFF)" : "(ON)");
            }).AddTo(this);
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
            Kernel.SceneTransitionService.ToGame();
        }


        private void OnBGMMuteClicked()
        {
            Kernel.UserSettingsService.MuteBGM = !Kernel.UserSettingsService.MuteBGM;
        }


        private void OnSFXMuteClicked()
        {
            Kernel.UserSettingsService.MuteSFX = !Kernel.UserSettingsService.MuteSFX;
        }        
    }
}
