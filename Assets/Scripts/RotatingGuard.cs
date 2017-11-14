using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingGuard : MonoBehaviour {

    public bool directionUp;
    public bool directionLeft;
    public bool directionDown;
    public bool directionRight;
    public float timeBetweenRotation;

    private float timeSinceLastRotation;
    private int currentDirectionIndex;
    private CatchPlayer ChildCatchPlayer;
    private SpriteRenderer spriteR;
    private Sprite[] sprites;
    private List<string> directions = new List<string>();

    void Start () {
        if (directionUp) directions.Add("up");
        if (directionLeft) directions.Add("left");
        if (directionDown) directions.Add("down");
        if (directionRight) directions.Add("right");

        ChildCatchPlayer = this.gameObject.transform.GetChild(0).GetComponent<CatchPlayer>();

        spriteR = gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("guard");
        currentDirectionIndex = 0;
        rotate(directions[0]);
    }
	
	void Update () {
        timeSinceLastRotation += Time.deltaTime;

        if (timeSinceLastRotation >= timeBetweenRotation)
        {
            currentDirectionIndex = (currentDirectionIndex + 1) % directions.Count;
            rotate(directions[currentDirectionIndex]);
            timeSinceLastRotation = 0;
        }
    }

    private void rotate(string direction)
    {
        if(direction == "up")
        {
            ChildCatchPlayer.turn(180);
            spriteR.sprite = sprites[6];
        } 
        else if(direction == "left")
        {
            ChildCatchPlayer.turn(270);
            spriteR.sprite = sprites[0];
        }
        else if (direction == "down")
        {
            ChildCatchPlayer.turn(0);
            spriteR.sprite = sprites[2];
        }
        else if (direction == "right")
        {
            ChildCatchPlayer.turn(90);
            spriteR.sprite = sprites[5];
        }
    }

    public void resetGuard()
    {
        currentDirectionIndex = 0;
        rotate(directions[0]);
        timeSinceLastRotation = 0;
    }
}
