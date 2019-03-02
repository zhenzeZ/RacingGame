using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use this to change scenes in unity
using UnityEngine.SceneManagement;

public class TrackMenu : MonoBehaviour {

	public void Track1Button()
	{
        Debug.Log("map 1");
        SceneManager.LoadScene(sceneName:"GameplayMap1");
	}
	public void Track2Button()
	{
        Debug.Log("map 2");
		SceneManager.LoadScene(sceneName: "GameplayMap2");
	}
}