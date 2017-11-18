using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingGuard : MonoBehaviour {

    public enum Direction
    {
        Up, Left, Down, Right
    }

    public Direction startDirection;
    public bool directionUp;
    public bool directionLeft;
    public bool directionDown;
    public bool directionRight;
    public float timeBetweenRotation;

    private float timeSinceLastRotation;
    private int currentDirectionIndex;
    private CatchPlayer ChildCatchPlayer;
    private GameObject ChildCatchIcon;
    private SpriteRenderer spriteR;
    private Sprite[] sprites;
    private List<string> directions = new List<string>();

    private const string up = "up", left = "left", down = "down", right = "right";

    [HideInInspector]
    public bool catchingPlayer;

    void Start () {
        if (directionUp) directions.Add(up);
        if (directionLeft) directions.Add(left);
        if (directionDown) directions.Add(down);
        if (directionRight) directions.Add(right);

        ChildCatchPlayer = this.gameObject.transform.GetChild(0).GetComponent<CatchPlayer>();
        ChildCatchIcon = this.gameObject.transform.GetChild(1).gameObject;

        spriteR = gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("guard");

        currentDirectionIndex = getStartDirection();
        rotate(directions[currentDirectionIndex]);

        catchingPlayer = false;
    }
	
	void Update () {
        if (!catchingPlayer)
        {
            timeSinceLastRotation += Time.deltaTime;

            if (timeSinceLastRotation >= timeBetweenRotation)
            {
                currentDirectionIndex = (currentDirectionIndex + 1) % directions.Count;
                rotate(directions[currentDirectionIndex]);
                timeSinceLastRotation = 0;
            }
        }
        else
        {
            ChildCatchIcon.SetActive(true);
        }
    }

    private int getStartDirection()
    {
        if (startDirection == Direction.Up && directions.Contains(up))
        {
            return directions.IndexOf(up);
        }
        else if (startDirection == Direction.Left && directions.Contains(left))
        {
            return directions.IndexOf(left);
        }
        else if (startDirection == Direction.Down && directions.Contains(down))
        {
            return directions.IndexOf(down);
        }
        else if (startDirection == Direction.Right && directions.Contains(right))
        {
            return directions.IndexOf(right);
        }
        else
        {
            return 0;
        }
    }

    private void rotate(string direction)
    {
        if(direction == up)
        {
            ChildCatchPlayer.turn(180);
            spriteR.sprite = sprites[6];
        } 
        else if(direction == left)
        {
            ChildCatchPlayer.turn(270);
            spriteR.sprite = sprites[0];
        }
        else if (direction == down)
        {
            ChildCatchPlayer.turn(0);
            spriteR.sprite = sprites[2];
        }
        else if (direction == right)
        {
            ChildCatchPlayer.turn(90);
            spriteR.sprite = sprites[5];
        }
    }

    public void resetGuard()
    {
        currentDirectionIndex = getStartDirection();
        rotate(directions[currentDirectionIndex]);
        timeSinceLastRotation = 0;
        catchingPlayer = false;
        ChildCatchIcon.SetActive(false);
        PlayerState.canCatch = PlayerState.GuardCanCatch.Yes;
    }
}
