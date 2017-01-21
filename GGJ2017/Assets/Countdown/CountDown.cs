using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

	public float timeLength = 60f;
	Text timeText;

	// Use this for initialization
	void Start () {
		timeText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateTimer(){
		if(timeLength > 0){
			//timeLength -= 
		}
	}
}
