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

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
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

    public void EndDialogue()
    {
        print("End of conversation");
        animator.SetBool("IsOpen", false);
        FindObjectOfType<PlayerController>().enable();
    }
}
