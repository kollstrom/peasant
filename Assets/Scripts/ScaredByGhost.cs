using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredByGhost : MonoBehaviour {

    private GameObject spaceButtonImage;
    private bool ghostInTrigger = false;
    private Vector2 ghostPosition;
    private MovingGuard parentScript;

    // Use this for initialization
    void Start () {
        spaceButtonImage = GameObject.Find("/UI/Canvas/SpaceButton");
        spaceButtonImage.SetActive(false);
        parentScript = transform.parent.gameObject.GetComponent<MovingGuard>();
    }
	
	// Update is called once per frame
	void Update () {

        if (ghostInTrigger && Input.GetKeyDown("space") && parentScript.state == MovingGuard.GuardState.Patrolling)
        {
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
