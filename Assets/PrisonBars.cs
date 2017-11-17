using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonBars : MonoBehaviour {

    public GameObject spaceButtonImage;
    public DialogueManager dialogManager;
    public Prisoner prisoner;

    private bool playerInTrigger;
    private float timer;
    private bool startTimer;

    private SpriteRenderer spriteR;

    // Use this for initialization
    void Start () {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        spaceButtonImage.SetActive(false);
        playerInTrigger = false;
        startTimer = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space) && playerInTrigger)
        {
            spriteR.enabled = false;
            spaceButtonImage.SetActive(false);
            startTimer = true;
        }

        if (startTimer)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5 && !dialogManager.GetComponent<DialogueManager>().HasMoreSentences())
            {
                sendEscapeCall();
                gameObject.SetActive(false);
            }
        }
    }

    private void sendEscapeCall()
    {
        prisoner.GetComponent<Prisoner>().escape = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerInTrigger = true;
            spaceButtonImage.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInTrigger = false;
            spaceButtonImage.SetActive(false);
        }
    }
}
