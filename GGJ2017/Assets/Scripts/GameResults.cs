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
		

	public void TriggerGameWin(int level){
		src.clip = winAudio;
		StartCoroutine("TriggerGameOverDelay", level);
	}

	public void TriggerGameMatch(int level){
		src.clip = matchAudio;
		StartCoroutine("TriggerGameOverDelay", level);
	}

	public void TriggerGameOver(){
		src.clip = loseAudio;
		StartCoroutine("TriggerGameOverDelay", 4);
	}


	IEnumerator TriggerGameOverDelay(int NextScene)
    {
        //Play game over sound
		//if (!src.isPlaying) {
		//	src.PlayOneShot (src.clip);
		//}

        //Wait for sound to finish
		//yield return new WaitForSeconds(src.clip.length);
		yield return new WaitForSeconds(2);

        //Transition to game over screen
		SceneManager.LoadScene(NextScene, LoadSceneMode.Single);
    }


}
