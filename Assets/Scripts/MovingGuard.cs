using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovingGuard : MonoBehaviour
{
    public enum GuardState
    {
        Patrolling, Scared, BackFromWall 
    }

    public float secondsAtWall;
    public float moveSpeed;
    public Vector3 endPoint;

    private Vector3 startPoint;
    private Vector3 currentPoint;
    [HideInInspector]
    public Vector3 lastScarePoint;
    [HideInInspector]
    public GuardState state;

    private Rigidbody2D myRigidbody;
    private Vector2 lastMoveDirection;
    private Vector2 vectorUp;
    private Vector2 vectorDown;
    private Vector2 vectorRight;
    private Vector2 vectorLeft;

    private float bounceFromWall = 0.1f;

    private Animator anim;
    private float lastXPosition;
    private float lastYPosition;
    private float lastXMove;
    private float lastYMove;


    void Start()
    {
        state = GuardState.Patrolling;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lastMoveDirection = new Vector2(0f, 0f);
        vectorUp = new Vector2(0, 1 * moveSpeed);
        vectorDown = new Vector2(0, -1 * moveSpeed);
        vectorRight = new Vector2(1 * moveSpeed, 0);
        vectorLeft = new Vector2(-1 * moveSpeed, 0);
        startPoint = transform.position;
        currentPoint = endPoint;
    }

    void Update()
    {
        if(transform.position == endPoint)
        {
            currentPoint = startPoint;
        }
        else if(transform.position == startPoint)
        {
            currentPoint = endPoint;
        }
        move();
    }

    private void move()
    {
        switch (state)
        {
            case GuardState.Patrolling:
                patroll();
                break;
            case GuardState.Scared:
                break;
            case GuardState.BackFromWall:
                backToPatroll();
                break;
        }

        float currentX = Truncate(transform.position.x);
        float currentY = Truncate(transform.position.y);
        float x = 0f; // Stays unchanged if not moving horizontally
        float y = 0f; // Stays unchanged if not moving vertically
        bool guardMoving = false;
        if (DifferenceBigEnough(lastXPosition, currentX))
        {
            if (lastXPosition > currentX)
            {
                // moving left
                x = -1f;
                guardMoving = true;
            }
            else if (lastXPosition < currentX)
            {
                // moving right
                x = 1f;
                guardMoving = true;
            }
        }

        if (DifferenceBigEnough(lastYPosition, currentY))
        {
            if (lastYPosition > currentY)
            {
                // moving down
                y = -1f;
                guardMoving = true;
            }
            else if (lastYPosition < currentY)
            {
                // moving up
                y = 1f;
                guardMoving = true;
            }
        }

        lastXPosition = currentX;
        lastYPosition = currentY;
        anim.SetFloat("LastMoveX", lastXMove);
        anim.SetFloat("LastMoveY", lastYMove);
        lastXMove = x;
        lastYMove = y;
        anim.SetFloat("MoveX", x);
        anim.SetFloat("MoveY", y);

        anim.SetBool("GuardMoving", guardMoving);
    }

    private bool DifferenceBigEnough(float one, float two)
    {
        float difference = Math.Abs(one - two);
        return difference > 0.015f;
    }

    private float Truncate(float value)
    {
        return (float) Math.Truncate(100 * value) / 100;
    }


    private void patroll()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentPoint, step);
    }

    private void backToPatroll()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, lastScarePoint, step);
        if(transform.position == lastScarePoint)
        {
            state = GuardState.Patrolling;
        }
        
    }

    public void scared(Vector2 v)
    {
        state = GuardState.Scared;
        float x = transform.position.x - v.x;
        float y = transform.position.y - v.y;
        float xyDiff = Mathf.Abs(x) - Mathf.Abs(y);

        if (xyDiff < 0)
        {
            if (y > Mathf.Abs(x)) //ghost is under guard
            {
                myRigidbody.velocity = vectorUp;
                lastMoveDirection = vectorUp;
            }
            else if (y < Mathf.Abs(x)) //ghost is over guard
            {
                myRigidbody.velocity = vectorDown;
                lastMoveDirection = vectorDown;
            }
        }
        else
        {
            if (x > Mathf.Abs(y)) //ghost is left of guard
            {
                myRigidbody.velocity = vectorRight;
                lastMoveDirection = vectorRight;
            }
            else if (x < Mathf.Abs(y)) //ghost is right guard
            {
                myRigidbody.velocity = vectorLeft;
                lastMoveDirection = vectorLeft;
            }

        }

    }

    public IEnumerator waitAtWall()
    {
        yield return new WaitForSecondsRealtime(secondsAtWall);
        state = GuardState.BackFromWall;
        StopCoroutine(waitAtWall());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == GuardState.Scared)  
        {
            myRigidbody.velocity = new Vector2(0f, 0f);
            if (lastMoveDirection == vectorUp)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - bounceFromWall);
                StartCoroutine(waitAtWall());
            }
            else if (lastMoveDirection == vectorDown)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + bounceFromWall);
                StartCoroutine(waitAtWall());
            }
            else if (lastMoveDirection == vectorRight)
            {
                transform.position = new Vector3(transform.position.x - bounceFromWall, transform.position.y);
                StartCoroutine(waitAtWall());
            }
            else if (lastMoveDirection == vectorLeft)
            {
                transform.position = new Vector3(transform.position.x + bounceFromWall, transform.position.y);
                StartCoroutine(waitAtWall());
            }
            

        }
    }


}
