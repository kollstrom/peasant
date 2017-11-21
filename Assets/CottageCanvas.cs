using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottageCanvas : MonoBehaviour {

    private Animator animator;

    float timer = 0;

    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 3)
        {
            animator.SetBool("show", true);
        }
        if (timer >= 8)
        {
            animator.SetBool("show", false);
        }
    }
}
