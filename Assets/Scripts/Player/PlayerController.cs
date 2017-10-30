﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public GhostController ghostCon;

    private Animator anim;
    private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("c"))
        {
            gameObject.SetActive(false);
            ghostCon.enable(transform.position);
            

        }
        moveplayer();
        
    }

    public void enable()
    {
        gameObject.SetActive(true);
        PlayerState.state = PlayerState.playerState.Player;
    }

    private void moveplayer()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

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

    public void caughtByGuard()
    {
        print("You have been caught by a guard");
    }

    
}
