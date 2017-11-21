using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisoner : MonoBehaviour {

    [HideInInspector]
    public bool escape;
    public float moveSpeed;
    public TeleportController teleportOut;
    public TeleportController teleportIn;
    public DialogueManager dialogManager;
    public SetGhostPlayble setGhostPlayable;
    public Animator towerAnimator;

    private Animator prisonerAnimator;
    private bool hasStartedDownwards = false;
    private bool hasStartedLeft = false;

    private float step;
    private Vector3 firstPoint;
    private Vector3 secondPoint;
    private Vector3 currentPoint;

    private bool fadeStarted = false;

    // Use this for initialization
    void Start () {
        escape = false;
        step = moveSpeed * Time.deltaTime;
        firstPoint = transform.position + new Vector3(0, -3, 0);
        secondPoint = firstPoint + new Vector3(-7, 0, 0);
        currentPoint = firstPoint;
        prisonerAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (escape == true)
        {
            StartWalkingDownwards();
            FindObjectOfType<PlayerController>().disable();
            //print(transform.position);
            if (transform.position.x <= secondPoint.x + 3.5f && !fadeStarted)
            {
                PlayerState.catched = PlayerState.Catched.Yes;
                fadeStarted = true;
            }
            if (transform.position == firstPoint)
            {
                StartWalkingLeft();
                currentPoint = secondPoint;
            }
            else if (transform.position == secondPoint)
            {
                dialogManager.EndDialogue();
                gameObject.SetActive(false);
                setGhostPlayable.gameObject.SetActive(true);
                teleportOut.gameObject.SetActive(true);
                teleportIn.gameObject.SetActive(false);
                towerAnimator.SetBool("isOpening", false);
                PlayerState.savedState = PlayerState.SavedState.Saved;
            }
            transform.position = Vector3.MoveTowards(transform.position, currentPoint, step);
        }

	}

    private void StartWalkingLeft()
    {
        if (!hasStartedLeft)
        {
            prisonerAnimator.SetBool("isWalkingDownwards", false);
        }
    }

    private void StartWalkingDownwards()
    {
        if (!hasStartedDownwards)
        {
            prisonerAnimator.SetBool("isWalkingDownwards", true);

        }
    }
}
