using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCanvasTrigger : MonoBehaviour {

	private BoxCollider2D trigger;
    private Animator animator;
    private GameObject player;
    private PolygonCollider2D playerCollider;
    private bool isTouching = false;

	void Start () {
        trigger = GetComponentInChildren<BoxCollider2D>();
        animator = GetComponentInChildren<Animator>();
        playerCollider = GameObject.Find("Player").GetComponent<PolygonCollider2D>();
	}
	
	void Update () {
        bool wasTouching = isTouching;
        if (trigger.IsTouching(playerCollider))
        {
            isTouching = true;
        }
        else {
            isTouching = false;
        }

        if (wasTouching != isTouching)
        {
            animator.SetBool("show", isTouching);
        }
	}
}
