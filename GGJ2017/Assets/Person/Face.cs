using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour {

	public Texture[] faces;
	public Renderer faceRenderer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
			OpenWide ();
		}
		if(Input.GetKeyDown(KeyCode.B)){
			OpenSmall ();
		}
		if(Input.GetKeyDown(KeyCode.C)){
			Closed ();
		}
		if(Input.GetKeyDown(KeyCode.D)){
			Blink ();
		}

	}

	public void LipSync(){
		faceRenderer.material.SetTexture("_MainTex", faces[Random.Range(0,2)]);
	}

	public void OpenWide(){
		faceRenderer.material.SetTexture("_MainTex", faces[0]);
	}

	public void OpenSmall(){
		faceRenderer.material.SetTexture("_MainTex", faces[1]);
	}

	public void Closed(){
		faceRenderer.material.SetTexture("_MainTex", faces[2]);
	}

	public void Blink(){
		faceRenderer.material.SetTexture("_MainTex", faces[3]);
	}

}
