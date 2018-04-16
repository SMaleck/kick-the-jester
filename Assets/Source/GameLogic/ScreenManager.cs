using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour {

    private const string SCENE_GAME = "Default";
    private const string SCENE_SHOP = "Shop";
    
	public void StartGame()
    {
        SceneManager.LoadSceneAsync(SCENE_GAME);
    }

    public void ShowShop()
    {
        SceneManager.LoadSceneAsync(SCENE_SHOP);
    }
}
