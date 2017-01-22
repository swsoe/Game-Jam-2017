using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour {

	public int characterType;

    public List<UnityEngine.Texture> textures;

    Animator animator;

	public Renderer characterRenderer;

	// Use this for initialization
	void Start ()
    {
		characterRenderer.material.mainTexture = textures[Random.Range(0, textures.Count)];
        //gameObject.GetComponent<UnityEngine.MeshRenderer>().material = textures[Random.Range(0, textures.Count)];
        characterType = Random.Range(0, 7);

        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
    {
        Collided();
    }

    void Collided()
    {
        //int result = 0;
		int result = InputManager.Instance.CheckCharacter(characterType);
		Debug.Log (result + " " + characterType);
        if (result > 0)
        {
            //trigger positive reaction
			animator.SetTrigger("Like");
        }
        if (result < 0)
        {
            //trigger negative reaction
			animator.SetTrigger("Hate");
        }
        
    }
}
