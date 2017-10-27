using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour {

    private bool ghostInTrigger = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ghostInTrigger && Input.GetKeyDown("space"))
        {
            transform.Rotate(0f, 0f, 180f);
            print("Guard has been scared");
            ghostInTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().caughtByGuard();

        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            ghostInTrigger = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            ghostInTrigger = false;

        }
    }
}
