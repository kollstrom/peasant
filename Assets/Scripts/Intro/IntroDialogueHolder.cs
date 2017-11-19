using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroDialogueHolder : MonoBehaviour {

    public Dialogue dialogue;

    private DialogueManager dialogueManager;
    public float timeBetweenDialogue;
    private float timeLeft;
    private bool hasStarted = false;

	void Start () {
        dialogueManager = FindObjectOfType<DialogueManager>();

	}
	
	void Update () {
        if (!hasStarted)
        {
            dialogueManager.StartDialogue(dialogue);
            timeLeft = timeBetweenDialogue;
            hasStarted = true;
        }
        else if (timeLeft <= 0)
        {
            if (dialogueManager.HasMoreSentences())
            {
                dialogueManager.DisplayNextSentence();
            }
            else 
            {
                gameObject.SetActive(false);
                SceneManager.LoadSceneAsync("World");
            }
        }
        timeLeft -= Time.deltaTime;
	}
}
