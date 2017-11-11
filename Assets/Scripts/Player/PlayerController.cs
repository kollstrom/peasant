using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public GhostController ghostCon;
    public Vector2 respawnPosition;

    private Animator anim;
    private Rigidbody2D myRigidbody;

    private bool playerMoving;
    private Vector2 lastMove;

	void Start () {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
        if (Input.GetKeyDown("c"))
        {
            disable();
            ghostCon.enable(transform.position);

        }
        moveplayer();
        
    }

    public void enable()
    {
        PlayerState.state = PlayerState.playerState.Player;
        GetComponent<Animator>().enabled = true;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void disable()
    {
        GetComponent<Animator>().enabled = false;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

    }

    private void moveplayer()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        playerMoving = false;

        if (Mathf.Abs(inputX) > 0.5f)
        {
            myRigidbody.velocity = new Vector2(inputX * moveSpeed, 0);
            playerMoving = true;
            lastMove = new Vector2(inputX, 0);
        }
        else if (Mathf.Abs(inputY) > 0.5f)
        {
            myRigidbody.velocity = new Vector2(0, inputY * moveSpeed);
            playerMoving = true;
            lastMove = new Vector2(0, inputY);
        }
        else
        {
            myRigidbody.velocity = new Vector2(0f, 0f);
        }

        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);

    }

    public void caughtByGuard()
    {
        print("You have been caught by a guard");
        transform.position = respawnPosition;
    }

    
}
