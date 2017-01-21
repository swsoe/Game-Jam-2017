using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour {

	public int characterType;

    public List<UnityEngine.Material> textures;

    public InputManager input = InputManager.Instance;

    public Animator animator;

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<UnityEngine.MeshRenderer>().material = textures[Random.Range(0, textures.Count)];
        characterType = Random.Range(0, 7);

        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collisionInfo)
    {
        Collided();
    }

    void Collided()
    {
        int result = 0;
        //result = input.CheckCharacter(characterType);
        if (result > 0)
        {
            //trigger positive reaction
        }
        if (result < 0)
        {
            //trigger negative reaction
        }
        animator.SetTrigger("Collider");
    }
}
