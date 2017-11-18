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
    }

    void Update()
    {

        if (dialogueHolder.HasStarted && dialogueHolder.IsFinished)
        {
            dialogueHolder.spaceButtonImage.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}