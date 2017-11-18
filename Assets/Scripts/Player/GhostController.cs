using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public PlayerController playerCon;
    public float moveSpeed;

    private Animator anim;
    private Rigidbody2D myRigidbody;

    private SpriteRenderer sr;
    private float nonOpaqueTimeLeft;
    public float nonOpaqueTime;

    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        nonOpaqueTimeLeft = 0;
        gameObject.SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetKeyDown("c") && PlayerState.ghostPlayable == PlayerState.GhostPlayable.Yes)
        {
            gameObject.SetActive(false);
            playerCon.enable();

        }
        if (Input.GetKeyDown("space"))
        {
            nonOpaqueTimeLeft = nonOpaqueTime;
        }
        if (nonOpaqueTimeLeft > 0)
        {
            nonOpaqueTimeLeft -= Time.deltaTime;
            sr.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, .5f);
        }
        moveplayer();
    }

    public void enable(Vector3 pos)
    {
        PlayerState.state = PlayerState.playerState.Ghost;
        gameObject.SetActive(true);
        transform.position = pos;
        FindObjectOfType<CameraController>().setCameraToGameObject(this.gameObject);
    }

    private void moveplayer()
    {
        float inputY = Input.GetAxisRaw("Vertical");
        float inputX = Input.GetAxisRaw("Horizontal");

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

        if (anim.isActiveAndEnabled)
        {
            anim.SetFloat("MoveY", inputY);
            anim.SetFloat("MoveX", inputX);
        }
    }


}

