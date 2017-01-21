using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour {

	public int characterType;

    public List<UnityEngine.Material> textures;

    public InputManager input = InputManager.Instance;

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<UnityEngine.MeshRenderer>().material = textures[Random.Range(0, textures.Count)];
        characterType = Random.Range(0, 7);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Collided()
    {
        int result = input.CheckCharacter(characterType);
        if (result > 0)
        {
            //trigger positive reaction
        }
        if (result < 0)
        {
            //trigger negative reaction
        }
    }
}
