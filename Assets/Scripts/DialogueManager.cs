using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Inspired by: Brackeys -> https://youtu.be/_nRzoTzeyxU

public class DialogueManager : MonoBehaviour {

    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
	}

    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        sentences.Clear();

        string[] dialogueSentences = dialogue.sentences;

        if((dialogue.name.Equals("Captain") 
            && PlayerState.savedState == PlayerState.SavedState.Saved)
           || (dialogue.name.Equals("GuardMother")
               && PlayerState.lunchState == PlayerState.LunchState.PickedUp)
           || (dialogue.name.Equals("HungryGuard")
               && PlayerState.lunchState == PlayerState.LunchState.PickedUp))
        {
            dialogueSentences = dialogue.afterSentences;
        }
            
        foreach(string sentence in dialogueSentences)
		{
			sentences.Enqueue(sentence);
		}

        DisplayNextSentence();

        if (dialogue.name.Equals("GuardMother"))
        {
            PlayerState.lunchState = PlayerState.LunchState.PickedUp;
        }
    }

    public bool DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return true;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        return false;
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public bool HasMoreSentences()
    {
        return sentences.Count > 0;
    }

    public int SentencesCount()
    {
        return sentences.Count;
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        FindObjectOfType<PlayerController>().enable();
    }
}
