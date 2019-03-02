using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use this to change scenes in unity
using UnityEngine.SceneManagement;

public class GameoverMenu : MonoBehaviour {

    public void BackToMainMenu() { 
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
}
