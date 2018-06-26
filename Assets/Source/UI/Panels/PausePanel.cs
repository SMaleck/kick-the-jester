using Assets.Source.App;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class PausePanel : AbstractPanel
    {
        [Header("Panel Properties")]
        [SerializeField] Button resumeButton;
        [SerializeField] Button retryButton;        

        [SerializeField] Toggle SFXMuteToggle;
        [SerializeField] Toggle BGMMuteToggle;

        public override void Setup()            
        {
            base.Setup();

            // Toggle this panel on/off depending on pause state
            App.Cache.Kernel.AppState.IsPausedProperty
                           .Subscribe((bool isPaused) => 
                           {
                               if (isPaused)
                               {
                                   Show();
                               }
                               else
                               {
                                   Hide();
                               }
                           })
                           .AddTo(this);

            resumeButton.OnClickAsObservable().Subscribe(_ => OnResumeClicked());
            retryButton.OnClickAsObservable().Subscribe(_ => OnRetryClicked());

            BGMMuteToggle.isOn = !App.Cache.Kernel.UserSettings.MuteBGM;
            SFXMuteToggle.isOn = !App.Cache.Kernel.UserSettings.MuteSFX;

            BGMMuteToggle.OnValueChangedAsObservable().Subscribe(_ => OnBGMMuteClicked());
            SFXMuteToggle.OnValueChangedAsObservable().Subscribe(_ => OnSFXMuteClicked());                       
        }


        private void OnResumeClicked()
        {
            App.Cache.userControl.TooglePause();
        }


        private void OnRetryClicked()
        {
            App.Cache.Kernel.SceneTransitionService.ToGame();
        }


        private void OnBGMMuteClicked()
        {
            App.Cache.Kernel.UserSettings.MuteBGM = !BGMMuteToggle.isOn;
        }


        private void OnSFXMuteClicked()
        {
            App.Cache.Kernel.UserSettings.MuteSFX = !SFXMuteToggle.isOn;
        }        
    }
}
