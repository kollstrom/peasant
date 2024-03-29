﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OutroTrigger : MonoBehaviour {

    private Animator[] animators;

    void Start()
    {
        animators = GetComponentsInChildren<Animator>();
        animators[1].SetBool("fadeOut", true);
        animators[2].SetBool("fadeOut", true);
    }

    public void showDialog(bool b)
    {
        animators[0].SetBool("show", b);
        animators[1].SetBool("fadeOut", false);
        StartCoroutine(ShowTextTwo());
    }

    private IEnumerator ShowTextTwo()
    {
        yield return new WaitForSecondsRealtime(5f);
        animators[1].SetBool("fadeOut", true);
        yield return new WaitForSecondsRealtime(0.5f);
        animators[2].SetBool("fadeOut", false);
        yield return new WaitForSecondsRealtime(5f);
        animators[2].SetBool("fadeOut", true);
    }
}