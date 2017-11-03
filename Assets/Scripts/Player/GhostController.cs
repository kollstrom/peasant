using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public PlayerController playerCon;
    public float moveSpeed;

    private Animator anim;
    private Rigidbody2D myRigidbody;

    SpriteRenderer sr;

    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        sr.color = new Color(1f, 1f, 1f, .5f);
        if (Input.GetKeyDown("c"))
        {
            gameObject.SetActive(false);
            playerCon.enable();

        }
        moveplayer();
    }

    public void enable(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
        PlayerState.state = PlayerState.playerState.Ghost;
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


}

