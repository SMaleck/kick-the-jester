using Assets.Source.App;
using Assets.Source.Behaviours.MainCamera;
using Assets.Source.Models;
using UnityEngine.SceneManagement;

namespace Assets.Source.Service
{
    public class SceneTransitionService
    {
        private string nextScene = "";

        private ScreenFader screenFader;
        private ScreenFader ScreenFader
        {
            get
            {
                if (screenFader == null)
                {
                    screenFader = App.Cache.mainCamera.GetComponentInChildren<ScreenFader>();
                }

                return screenFader;
            }
        }


        public SceneTransitionService()
        {            
            ScreenFader.OnFadeOutComplete(Execute);
        }


        private event NotifyEventHandler _OnStartLoading = delegate { };
        public void OnStartLoading(NotifyEventHandler handler)
        {
            _OnStartLoading += handler;
        }


        private void Execute()
        {
            if (string.IsNullOrEmpty(nextScene))
            {
                return;
            }

            _OnStartLoading();
            SceneManager.LoadSceneAsync(nextScene);
        }


        private void CheckScreenFader()
        {
            if(screenFader == null)
            {
                ScreenFader.OnFadeOutComplete(Execute);
            }
        }


        /* ---------------------------------------------------------------------------------------- */
        #region SCENE TRANSITIONS

        public void ToGame()
        {
            CheckScreenFader();
            nextScene = Constants.SCENE_GAME;
            ScreenFader.FadeOut();
        }


        public void ToShop()
        {
            CheckScreenFader();
            nextScene = Constants.SCENE_SHOP;
            ScreenFader.FadeOut();
        }

        #endregion
    }
}
