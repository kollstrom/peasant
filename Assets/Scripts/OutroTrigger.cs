using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OutroTrigger : MonoBehaviour {

    private Animator[] animators;

    private Text[] texts;

    void Start()
    {
        animators = GetComponentsInChildren<Animator>();
        texts = GetComponentsInChildren<Text>();
        print(texts);
        print(animators.Length);
        animators[1].SetBool("fadeOut", true);
        animators[2].SetBool("fadeOut", true);
    }

    public void showDialog(bool b)
    {
        animators[0].SetBool("show", b);
        animators[1].SetBool("fadeOut", false);
        //texts[0].enabled = true;
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
