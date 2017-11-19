using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DialogueTrigger
public class DialogueHolder : MonoBehaviour {
    
    public GameObject spaceButtonImage;

    public Dialogue dialogue;

    private SoundManager sfxManager;

    private bool isFinished = true;
    private bool hasStarted = false;

    public bool HasStarted
    {
        get
        {
            return hasStarted;
        }
    }

    public bool IsFinished
    {
        get
        {
            return isFinished;
        }
    }

    private bool isWithinReach = false;

    private bool isGhost = false;

    public Animator prisonTowerAnim;

    public GameObject teleporterIntoDungeon;

	void Start () {
        spaceButtonImage.SetActive(false);
        sfxManager = FindObjectOfType<SoundManager>();
        if (teleporterIntoDungeon != null)
        {
            teleporterIntoDungeon.SetActive(false);
        }

	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("c") && PlayerState.ghostPlayable == PlayerState.GhostPlayable.Yes)
        {
            spaceButtonImage.SetActive(false);
            if (isGhost)
            {
                sfxManager.swapToWomanSound.Play();
            }
            else 
            {
                sfxManager.swapToGhostSound.Play();
            }
            isGhost = !isGhost;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !isFinished)
        {
            DialogueManager diaMan = FindObjectOfType<DialogueManager>();
            isFinished = diaMan.DisplayNextSentence();
            if (!isFinished)
            {
                if (!(dialogue.name.Equals("GuardMother") 
                     && diaMan.SentencesCount() == 0) &&
                    !(dialogue.name.Equals("Prisoner")
                       && diaMan.SentencesCount() == 0))
                {
                    sfxManager.nextSound.Play();
                }
                else if  (dialogue.name.Equals("GuardMother"))
                {
                    sfxManager.obtainedLunchSound.Play();
                }
                else if (dialogue.name.Equals("Prisoner"))
                {
                    sfxManager.celebrationSound.Play();
                }
            }
            else if (dialogue.name.Equals("HungryGuard") &&
                         PlayerState.lunchState == PlayerState.LunchState.PickedUp)
            {
                teleporterIntoDungeon.SetActive(true);
                prisonTowerAnim.SetBool("isOpening", true);
                sfxManager.openDoorSound.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) && isWithinReach)
        {
            // play next sound
            FindObjectOfType<PlayerController>().disable();
            isFinished = false;
            TriggerDialogue();
            sfxManager.nextSound.Play();
        }
	}

    private bool dialogueHasMoreSentences()
    {
        return FindObjectOfType<DialogueManager>().HasMoreSentences();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // Show "Space" icon
            spaceButtonImage.SetActive(true);
            isWithinReach = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Hide "Space" icon
        if(collision.gameObject.name == "Player")
        {
            spaceButtonImage.SetActive(false);
            isWithinReach = false;
        }
    }

    public void TriggerDialogue()
    {
        hasStarted = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
