using UnityEngine;
using System.Collections;

public class Charisma : MonoBehaviour {

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

    //The bar object
    private UnityEngine.UI.Slider scrollBar;

	// Use this for initialization
	void Start ()
    {
        scrollBar = gameObject.GetComponent<UnityEngine.UI.Slider>();
        scrollBar.minValue = 0;
        scrollBar.maxValue = charismaMax;
        scrollBar.value = charisma;
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
                    charisma = 0;
                    drain = false;
                }
                time = Time.time;
            }
            Percentage = (float)charisma / (float)charismaMax;

            scrollBar.value = charisma;

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
