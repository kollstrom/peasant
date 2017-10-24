using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;

    private Animator anim;
    private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
       moveplayer();
    }


    void moveplayer()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        if (inputX == 1 || inputX == -1)
        {
            inputY = 0f;
            myRigidbody.velocity = new Vector2(inputX * moveSpeed, 0);
        }
        else if (inputY == 1 || inputY == -1)
        {
            inputX = 0f;
            myRigidbody.velocity = new Vector2(0, inputY * moveSpeed);
        }

        if (inputX == 0 && inputY == 0)
        {
            myRigidbody.velocity = new Vector2(0f, 0f);
        }

        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
    }
}
