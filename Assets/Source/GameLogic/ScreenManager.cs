using Assets.Source.App;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour {
    
	public void StartGame()
    {
        SceneManager.LoadSceneAsync(Constants.SCENE_GAME);
    }

    public void ShowShop()
    {
        SceneManager.LoadSceneAsync(Constants.SCENE_SHOP);
    }
}
