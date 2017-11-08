using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogHolder : MonoBehaviour {

    public string dialogue;

    public GameObject imageGameObject;

	// Use this for initialization
	void Start () {
        imageGameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            // Show "Space" icon
            imageGameObject.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                // Show dialogue box
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Hide "Space" icon
        if(collision.gameObject.name == "Player")
        {
            imageGameObject.SetActive(false);
        }
    }
}
