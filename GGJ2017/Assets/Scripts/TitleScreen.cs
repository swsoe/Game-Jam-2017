using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class TitleScreen : MonoBehaviour {

	bool isFirstTime = true;

	public void StartLevel(int level)
    {
		if(isFirstTime){
			PlayerPrefs.SetInt("win", 0);
			PlayerPrefs.Save ();
			isFirstTime = false;
		}
		SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

}
