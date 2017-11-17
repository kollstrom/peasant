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

    private float step;
    private Vector3 firstPoint;
    private Vector3 secondPoint;
    private Vector3 currentPoint;

    // Use this for initialization
    void Start () {
        escape = false;
        step = moveSpeed * Time.deltaTime;
        firstPoint = transform.position + new Vector3(0, -3, 0);
        secondPoint = firstPoint + new Vector3(-7, 0, 0);
        currentPoint = firstPoint;
    }
	
	// Update is called once per frame
	void Update () {
        if (escape == true)
        {
            FindObjectOfType<PlayerController>().disable();
            if (transform.position == firstPoint)
            {
                currentPoint = secondPoint;
            }
            else if (transform.position == secondPoint)
            {
                dialogManager.EndDialogue();
                gameObject.SetActive(false);
                setGhostPlayable.gameObject.SetActive(true);
                teleportOut.gameObject.SetActive(true);
                teleportIn.gameObject.SetActive(false);
                PlayerState.savedState = PlayerState.SavedState.Saved;
            }
            transform.position = Vector3.MoveTowards(transform.position, currentPoint, step);
        }

	}

    
}
