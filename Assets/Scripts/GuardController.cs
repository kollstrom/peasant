using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{

    public float moveSpeed;

    private bool ghostInTrigger = false;
    private CircleCollider2D circleCol;
    private BoxCollider2D boxCol;
    private Vector2 ghostPosition;
    private Rigidbody2D myRigidbody;
    private bool moving;
    private Vector2 lastMoveDirection;
    private Vector2 vectorUp;
    private Vector2 vectorDown;
    private Vector2 vectorRight;
    private Vector2 vectorLeft;

    private float bounceFromWall = 0.1f;


    void Start()
    {
        circleCol = GetComponent<CircleCollider2D>();
        boxCol = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        moving = false;
        lastMoveDirection = new Vector2(0f, 0f);
        vectorUp = new Vector2(0, 1 * moveSpeed);
        vectorDown = new Vector2(0, -1 * moveSpeed);
        vectorRight = new Vector2(1 * moveSpeed, 0);
        vectorLeft = new Vector2(-1 * moveSpeed, 0);
    }

    void Update()
    {
        switch (PlayerState.state)
        {
            case PlayerState.playerState.Player:
                circleCol.enabled = false;
                boxCol.enabled = true;
                break;
            case PlayerState.playerState.Ghost:
                circleCol.enabled = true;
                boxCol.enabled = false;
                break;
        }

        if (ghostInTrigger && Input.GetKeyDown("space"))
        {
            print("Guard has been scared");
            ghostInTrigger = false;
            move(ghostPosition);
        }
    }

    private void move(Vector2 v)
    {
        float x = transform.position.x - v.x;
        float y = transform.position.y - v.y;
        float xyDiff = Mathf.Abs(x) - Mathf.Abs(y);

        if (xyDiff < 0)
        {
            if (y > Mathf.Abs(x)) //ghost is under guard
            {
                transform.localEulerAngles = new Vector3(0, 0, 180);
                myRigidbody.velocity = vectorUp;
                lastMoveDirection = vectorUp;
                moving = true;
            }
            else if (y < Mathf.Abs(x)) //ghost is over guard
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
                myRigidbody.velocity = vectorDown;
                lastMoveDirection = vectorDown;
                moving = true;
            }
        }
        else
        {
            if (x > Mathf.Abs(y)) //ghost is left of guard
            {
                transform.localEulerAngles = new Vector3(0, 0, 90);
                myRigidbody.velocity = vectorRight;
                lastMoveDirection = vectorRight;
                moving = true;
            }
            else if (x < Mathf.Abs(y)) //ghost is right guard
            {
                transform.localEulerAngles = new Vector3(0, 0, 270);
                myRigidbody.velocity = vectorLeft;
                lastMoveDirection = vectorLeft;
                moving = true;
            }

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        myRigidbody.velocity = new Vector2(0f, 0f);
        if (moving)
        {
            if (lastMoveDirection == vectorUp)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - bounceFromWall);
            }
            else if (lastMoveDirection == vectorDown)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + bounceFromWall);
            }
            else if (lastMoveDirection == vectorRight)
            {
                transform.position = new Vector3(transform.position.x - bounceFromWall, transform.position.y);
            }
            else if (lastMoveDirection == vectorLeft)
            {
                transform.position = new Vector3(transform.position.x + bounceFromWall, transform.position.y);
            }
        }

        moving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().caughtByGuard();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            ghostInTrigger = true;
            ghostPosition = collision.transform.position;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            ghostInTrigger = false;

        }
    }


}