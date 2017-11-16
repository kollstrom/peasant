using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGuard : MonoBehaviour
{
    public enum GuardState
    {
        Patrolling, Scared, BackFromWall 
    }

    public float secondsAtWall;
    public float moveSpeed;
    public Vector3 target;
    private Vector3 endPoint;

    private SpriteRenderer spriteR;
    private Sprite[] sprites;
    private CatchPlayer ChildCatchPlayer;

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

    private Animator anim;
    private float lastX;
    private float lastY;


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

        spriteR = gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("guard");
        ChildCatchPlayer = this.gameObject.transform.GetChild(0).GetComponent<CatchPlayer>();
        endPoint = new Vector3(startPoint.x + target.x, startPoint.y + target.y, startPoint.z + target.z);
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

    public void resetGuard()
    {
        transform.position = startPoint;
        currentPoint = endPoint;
        myRigidbody.velocity = new Vector2(0f, 0f);
        state = GuardState.Patrolling;
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

        float currentX = transform.position.x;
        float currentY = transform.position.y;
        float x;
        float y;
        float xDiff = lastX - currentX;
        float yDiff = lastY - currentY;
        if (xDiff > moveSpeed / 100)
        {
            // moving left
            x = -1f;
            ChildCatchPlayer.turn(270);
        }
        else if (xDiff < -moveSpeed / 100)
        {
            // moving right
            x = 1f;
            ChildCatchPlayer.turn(90);
        }
        else 
        {
            // not moving horizontally
            x = 0f;
        }
        if (yDiff > moveSpeed / 100)
        {
            // moving down
            y = -1f;
            ChildCatchPlayer.turn(0);
        }
        else if (yDiff < -moveSpeed/100)
        {
            // moving up
            y = 1f;
            ChildCatchPlayer.turn(180);
        }
        else 
        {
            // not moving vertically
            y = 0f;
        }
        lastX = transform.position.x;
        lastY = transform.position.y;
        anim.SetFloat("MoveX", x);
        anim.SetFloat("MoveY", y);
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
        anim.enabled = true;
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
                anim.enabled = false;
                spriteR.sprite = sprites[6];
                StartCoroutine(waitAtWall());
            }
            else if (lastMoveDirection == vectorDown)
            {
                anim.enabled = false;
                spriteR.sprite = sprites[2];
                StartCoroutine(waitAtWall());

            }
            else if (lastMoveDirection == vectorRight)
            {
                anim.enabled = false;
                spriteR.sprite = sprites[5];
                StartCoroutine(waitAtWall());
            }
            else if (lastMoveDirection == vectorLeft)
            {
                anim.enabled = false;
                spriteR.sprite = sprites[0];
                StartCoroutine(waitAtWall());
            }
            

        }
    }


}
