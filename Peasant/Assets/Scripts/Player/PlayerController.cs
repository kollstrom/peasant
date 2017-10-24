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
            
        print("X: " + inputX);
        print("Y: " + inputY);

        if (Mathf.Abs(inputX) > 0.5f)
        {
            myRigidbody.velocity = new Vector2(inputX * moveSpeed, 0);
        }
        else if (Mathf.Abs(inputY) > 0.5f)
        {
            myRigidbody.velocity = new Vector2(0, inputY * moveSpeed);
        }
        else
        {
            myRigidbody.velocity = new Vector2(0f, 0f);
        }

        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
    }

}
