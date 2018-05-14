using Assets.Source.App;
using Assets.Source.Behaviours.MainCamera;
using Assets.Source.Models;
using UnityEngine.SceneManagement;

namespace Assets.Source.Service
{
    public class SceneTransitionService
    {
        private string nextScene = "";


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
