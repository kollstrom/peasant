using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroTrigger : MonoBehaviour {

    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void showDialog(bool b)
    {
        animator.SetBool("show", b);
    }
}
