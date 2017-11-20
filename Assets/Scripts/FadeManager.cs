using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour {

    private Animator fadeAnimator;
    public float fadeInTime;

	void Start () {
        fadeAnimator = GetComponent<Animator>();
	}
	
	void Update () {
        if (PlayerState.catched == PlayerState.Catched.Yes)
        {
            fadeAnimator.SetBool("fadeOut", true);
            StartCoroutine(FadeIn());
            PlayerState.catched = PlayerState.Catched.No;
        }
	}

    private IEnumerator FadeIn()
    {
        yield return new WaitForSecondsRealtime(fadeInTime);
        fadeAnimator.SetBool("fadeOut", false);
    }
}
