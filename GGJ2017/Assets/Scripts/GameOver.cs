using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    public UnityEngine.AudioSource gameOverSoundSource;
    public UnityEngine.AudioClip gameOverSound;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public IEnumerator TriggerGameOver()
    {
        //Play game over sound
        if (!gameOverSoundSource.isPlaying)
            gameOverSoundSource.PlayOneShot(gameOverSound);

        //Wait for sound to finish
        yield return new WaitForSeconds(gameOverSound.length);

        //Transition to game over screen
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScreen");
    }
}
