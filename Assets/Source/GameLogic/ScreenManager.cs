using Assets.Source.App;
using Assets.Source.Behaviours.MainCamera;
using Assets.Source.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    private ScreenFader fader;

    private event NotifyEventHandler _OnStartLoading = delegate { };
    public void OnStartLoading(NotifyEventHandler handler)
    {
        _OnStartLoading += handler;
    }

    private void Start()
    {
        fader = Camera.main.GetComponentInChildren<ScreenFader>();
        fader.OnFadeComplete(Execute);
    }


    private string nextScene = "";
    private void Execute()
    {
        if (string.IsNullOrEmpty(nextScene))
        {
            return;
        }

        _OnStartLoading();
        SceneManager.LoadSceneAsync(nextScene);
    }


    /* --------------------------------------------------------- */
    #region SCENE LOADING

    public void StartGame()
    {
        nextScene = Constants.SCENE_GAME;
        fader.FadeOut();
    }


    public void ShowShop()
    {
        nextScene = Constants.SCENE_SHOP;
        fader.FadeOut();
    }

    #endregion
}
