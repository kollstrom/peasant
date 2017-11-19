using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{

    private DialogueHolder dialogueHolder;
    private SpriteRenderer sr;
    private Animator anim;

    void Start()
    {
        dialogueHolder = gameObject.GetComponentInChildren<DialogueHolder>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sr.color = new Color(1f, 1f, 1f, .5f);
    }

    void Update()
    {

        if (dialogueHolder.HasStarted && dialogueHolder.IsFinished)
        {
            anim.SetBool("isDoneTalking", true);
            dialogueHolder.spaceButtonImage.SetActive(false);
            StartCoroutine(DeactivateGhostCutscene());
        }
    }

    private IEnumerator DeactivateGhostCutscene()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        gameObject.SetActive(false);
    }
}