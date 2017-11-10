using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogHolder : MonoBehaviour {

    public string dialogue;

    public GameObject spaceButtonImage;

	void Start () {
        spaceButtonImage.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("c"))
        {
            playerIsActive = false;
            spaceButtonImage.SetActive(false);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // Show "Space" icon
            spaceButtonImage.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
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
            spaceButtonImage.SetActive(false);
        }
    }
}
