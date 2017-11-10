using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inspired by: Brackeys -> https://youtu.be/_nRzoTzeyxU

public class DialogueManager : MonoBehaviour {

    private Queue<string> sentences;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
	}

    public void StartDialogue (Dialogue dialogue)
    {
        print("Starting conversation with " + dialogue.name);
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
        print(sentence);
        return false;
    }

    public bool HasMoreSentences()
    {
        return sentences.Count > 0;
    }

    public void EndDialogue()
    {
        print("End of conversation");
        FindObjectOfType<PlayerController>().enable();
    }
}
