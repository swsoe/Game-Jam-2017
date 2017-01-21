using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class TimeUpEvent : UnityEvent{}

public class CountDown : MonoBehaviour {

	public float timeLength = 60f;

	public bool isCountDown = false;

	public TimeUpEvent timeUpEvent;

	private Text timeText;

	// Use this for initialization
	void Start () {
		timeText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isCountDown){
			UpdateTimer ();
		}
	}

	void UpdateTimer(){
		if (timeLength > 0) {
			timeLength -= Time.deltaTime;
		} else {
			timeLength = 0f;
			isCountDown = false;
			timeUpEvent.Invoke ();
		}
		timeText.text = ((int)timeLength).ToString ();



	}

    public void StartTimer()
    {
        isCountDown = true;
    }
}
