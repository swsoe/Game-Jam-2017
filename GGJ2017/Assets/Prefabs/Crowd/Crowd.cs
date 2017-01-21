using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour {

	public int characterType;

    public List<UnityEngine.Material> textures;

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<UnityEngine.MeshRenderer>().material = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
