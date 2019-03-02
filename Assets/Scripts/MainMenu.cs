using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use this to change scenes in unity
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
		SceneManager.LoadScene(sceneName:"GameplayMap1");
    }
    public void QuitGame()
    {
        Debug.Log("Game quit");
        Application.Quit();
    }
}
