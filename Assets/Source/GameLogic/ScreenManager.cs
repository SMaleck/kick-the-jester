using Assets.Source.App;
using Assets.Source.Behaviours.MainCamera;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    private ScreenFader fader;

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

        SceneManager.LoadSceneAsync(nextScene);
    }

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
}
