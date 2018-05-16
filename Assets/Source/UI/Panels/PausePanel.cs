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

        [SerializeField] Toggle SFXMuteToggle;
        [SerializeField] Toggle BGMMuteToggle;

        private void Awake()
        {
            App.Cache.RepoRx.GameStateRepository.StateProperty
                                                .TakeUntilDestroy(this)                                                
                                                .Subscribe(TooglePanel);

            resumeButton.OnClickAsObservable().Subscribe(_ => OnResumeClicked());
            retryButton.OnClickAsObservable().Subscribe(_ => OnRetryClicked());

            BGMMuteToggle.isOn = Kernel.UserSettingsService.MuteBGM;
            SFXMuteToggle.isOn = Kernel.UserSettingsService.MuteSFX;

            BGMMuteToggle.OnValueChangedAsObservable().Subscribe(_ => OnBGMMuteClicked());
            SFXMuteToggle.OnValueChangedAsObservable().Subscribe(_ => OnSFXMuteClicked());                       
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
