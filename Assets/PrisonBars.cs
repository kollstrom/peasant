using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonBars : MonoBehaviour {

    public GameObject spaceButtonImage;
    private bool playerInTrigger;
    public DialogueHolder dialog;

    // Use this for initialization
    void Start () {
        spaceButtonImage.SetActive(false);
        playerInTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space) && playerInTrigger)
        {
            gameObject.SetActive(false);
            spaceButtonImage.SetActive(false);
        }
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
