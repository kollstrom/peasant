using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DialogueTrigger
public class DialogueHolder : MonoBehaviour {
    
    public GameObject spaceButtonImage;

    public Dialogue dialogue;

    private bool isFinished = true;

    private bool isWithinReach = false;

	void Start () {
        spaceButtonImage.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("c"))
        {
            spaceButtonImage.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !isFinished)
        {
            isFinished = FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
        else if (Input.GetKeyUp(KeyCode.Space) && isWithinReach)
        {
            FindObjectOfType<PlayerController>().disable();
            isFinished = false;
            TriggerDialogue();
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

    private void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
