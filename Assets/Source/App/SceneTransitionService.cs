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

        public void ToGame()
        {         
            nextScene = Constants.SCENE_GAME;

            App.Cache.MainCamera.FadeOut(Execute);
        }


        public void ToShop()
        {            
            nextScene = Constants.SCENE_SHOP;

            App.Cache.MainCamera.FadeOut(Execute);
        }

        #endregion
    }
}
