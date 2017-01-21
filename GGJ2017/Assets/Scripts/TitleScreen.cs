using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

    private Level selectedLevel = Level.FirstAppearence;
    public UnityEngine.UI.Text textBox;

    // Use this for initialization
    void Start ()
    {
        textBox.text = selectedLevel.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Next()
    {
        selectedLevel += 1;
        if ((int)selectedLevel > 2)
            selectedLevel = (Level)0;
        textBox.text = selectedLevel.ToString();
    }

    public void Previous()
    {
        selectedLevel -= 1;
        if ((int)selectedLevel < 0)
            selectedLevel = (Level)2;
        textBox.text = selectedLevel.ToString();
    }

    public void StartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(selectedLevel.ToString());
    }

    private enum Level {FirstAppearence, TownHall, TheBigSpeech}

}
