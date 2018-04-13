using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour {
    
	public void StartGame()
    {
        SceneManager.LoadSceneAsync("Default");
    }

    public void ShowShop()
    {
        SceneManager.LoadSceneAsync("Shop");
    }
}
