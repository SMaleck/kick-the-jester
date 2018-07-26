using Assets.Source.App;
using UniRx;
using UnityEngine.SceneManagement;

namespace Assets.Source.App
{
    public class SceneTransitionService
    {
        private string nextScene = "";
        public BoolReactiveProperty IsLoadingProperty = new BoolReactiveProperty(false);
        

        public SceneTransitionService()
        {
            SceneManager.sceneLoaded += (Scene s, LoadSceneMode lm) => {
                IsLoadingProperty.Value = false;

                App.Cache.LoadingPanel.FadeIn();
            };
        }


        private void Execute()
        {
            if (string.IsNullOrEmpty(nextScene))
            {
                return;
            }

            IsLoadingProperty.Value = true;
            SceneManager.LoadSceneAsync(nextScene);
        }


        /* ---------------------------------------------------------------------------------------- */
        #region SCENE TRANSITIONS

        public void ToTitle()
        {
            nextScene = Constants.SCENE_TITLE;
            App.Cache.LoadingPanel.FadeOut(Execute);
        }


        public void ToGame()
        {         
            nextScene = Constants.SCENE_GAME;
            App.Cache.LoadingPanel.FadeOut(Execute);
        }

        #endregion
    }
}
