using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelStartTrigger : UnityEngine.Events.UnityEvent{ }

public class LevelStartCountdown : MonoBehaviour {

    public LevelStartTrigger levelStartCountdown;

    private float time = 5f;
    private bool countingDown;
    private UnityEngine.UI.Text textBox;

	// Use this for initialization
	void Start ()
    {
        textBox = gameObject.GetComponent<UnityEngine.UI.Text>();
        textBox.text = time.ToString();
        countingDown = true;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (countingDown)
        {
            if (Mathf.Round(time) > 0)
            {
                textBox.text = Mathf.Round(time).ToString();
            }
            else
            {
                textBox.text = "Type!";
                countingDown = false;
                levelStartCountdown.Invoke();
            }
        }
        else
        {
            if (time < 0)
				DestroyImmediate(transform.parent.gameObject);
        }

        time -= Time.deltaTime;
		
	}
}
