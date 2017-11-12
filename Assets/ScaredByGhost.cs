using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredByGhost : MonoBehaviour {

    public GameObject spaceButtonImage;

    private bool ghostInTrigger = false;
    private Vector2 ghostPosition;
    private MovingGuard parentScript;

    // Use this for initialization
    void Start () {
        spaceButtonImage.SetActive(false);
        parentScript = transform.parent.gameObject.GetComponent<MovingGuard>();
        Debug.Log(parentScript.name);
    }
	
	// Update is called once per frame
	void Update () {

        if (ghostInTrigger && Input.GetKeyDown("space") && parentScript.state == MovingGuard.GuardState.Patrolling)
        {
            print("Guard has been scared");
            ghostInTrigger = false;
            parentScript.scared(ghostPosition);
            parentScript.lastScarePoint = transform.parent.position;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            spaceButtonImage.SetActive(true);
            ghostInTrigger = true;
            ghostPosition = collision.transform.position;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            spaceButtonImage.SetActive(false);
            ghostInTrigger = false;

        }
    }
}
