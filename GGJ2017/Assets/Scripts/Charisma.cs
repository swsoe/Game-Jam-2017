using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;


[System.Serializable]
public class WinEvent : UnityEvent{}

[System.Serializable]
public class MatchEvent : UnityEvent{}

[System.Serializable]
public class LoseEvent : UnityEvent{}

public class Charisma : MonoBehaviour {

	public WinEvent winEvent;
	public MatchEvent matchEvent;
	public LoseEvent loseEvent;

    //Starting charisma
    public int charisma = 500;

    //Charisma max
    public int charismaMax = 1000;

    //Charisma drain amount
    public int charismaDrain = 1;

    //Charisma drain tick length in seconds
    public float tickLength = 0.2F;

    public float time;
    public float currentTime;

    //Enables/Disables the draining of 
    public bool drain = false;

    public float Percentage;

	//
	public Image fill;

	// Use this for initialization
	void Start ()
    {
		charisma = charismaMax / 4;

		if(PlayerPrefs.GetInt("win") == 1){
			charisma = charismaMax / 2;
		}
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (drain)
        {
            currentTime = Time.time;
            if (currentTime - time >= tickLength)
            {
                charisma = charisma - charismaDrain;
                if (charisma < 0)
                {
					loseEvent.Invoke ();
                    charisma = 0;
                    drain = false;
                }
                time = Time.time;
            }
            Percentage = (float)charisma / (float)charismaMax;

			fill.fillAmount = Percentage;

			if(Percentage > .95F){
				PlayerPrefs.SetInt("win", 1);
				PlayerPrefs.Save ();
				winEvent.Invoke ();
			}

        }
        
	}

	public void TimeUpResults(){
		PlayerPrefs.SetInt("win", 0);
		PlayerPrefs.Save ();
		if (Percentage > .2f) {
			matchEvent.Invoke ();
		} else {
			loseEvent.Invoke ();
		}
	}

    public void startBar()
    {
        drain = true;
        time = Time.time;
    }

    public void addPoints(int points)
    {
        charisma += points;
    }
}
