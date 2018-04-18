using Assets.Source.App;
using Assets.Source.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    // Event to notify when we start switching to another scene
    private event NotifyEventHandler _OnSwitching = delegate { };
    public void OnSwitching(NotifyEventHandler handler)
    {
        _OnSwitching += handler;        
    }


    public void StartGame()
    {
        _OnSwitching();
        SceneManager.LoadSceneAsync(Constants.SCENE_GAME);
    }


    public void ShowShop()
    {
        _OnSwitching();
        SceneManager.LoadSceneAsync(Constants.SCENE_SHOP);
    }
}
