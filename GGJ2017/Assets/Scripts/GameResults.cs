using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameResults : MonoBehaviour {

	AudioSource src;

	public AudioClip winAudio;
	public AudioClip matchAudio;
	public AudioClip loseAudio;

	// Use this for initialization
	void Start ()
    {
		src = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}
		

	public void TriggerGameWin(){
		src.clip = winAudio;
		StartCoroutine("TriggerGameOverDelay", "GameOverScreen");
	}

	public void TriggerGameMatch(){
		src.clip = matchAudio;
		StartCoroutine("TriggerGameOverDelay", "GameOverScreen");
	}

	public void TriggerGameOver(){
		src.clip = loseAudio;
		StartCoroutine("TriggerGameOverDelay", "GameOverScreen");
	}


	IEnumerator TriggerGameOverDelay(string NextScene)
    {
        //Play game over sound
		if (!src.isPlaying) {
			src.PlayOneShot (src.clip);
		}

        //Wait for sound to finish
		yield return new WaitForSeconds(src.clip.length);

        //Transition to game over screen
		SceneManager.LoadScene(NextScene);
    }


}
