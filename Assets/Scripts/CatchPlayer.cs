using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPlayer : MonoBehaviour {

    private GuardContainer guardCont;
    private BoxCollider2D box2d;
    private GameObject parent;
    private bool isScarableGuard;
    public LayerMask layermask;

    // Use this for initialization
    void Start () {
        guardCont = transform.parent.parent.gameObject.GetComponent<GuardContainer>();
        box2d = transform.gameObject.GetComponent<BoxCollider2D>();
        parent = transform.parent.gameObject;
        if(parent.tag == "MovingGuard" || parent.tag == "StandingGuard")
        {
            isScarableGuard = true;
        }
        else
        {
            isScarableGuard = false;
        }
}
	
	// Update is called once per frame
	void Update () {
        if (isScarableGuard)
        {
            parent.transform.GetChild(1).GetComponent<PolygonCollider2D>().enabled = false;
        }

        parent.GetComponent<BoxCollider2D>().enabled = false;
        box2d.enabled = false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 30, layermask);

        box2d.enabled = true;
        parent.GetComponent<BoxCollider2D>().enabled = true;

        if (isScarableGuard)
        {
            parent.transform.GetChild(1).GetComponent<PolygonCollider2D>().enabled = true;
        }

        if (hit.collider != null)
        {
            box2d.offset = new Vector2(box2d.offset.x, -hit.distance/2);
            box2d.size = new Vector2(box2d.size.x, hit.distance);
            //print(hit.collider.name);
        }
    }


    public void turn(float direction)
    {
        transform.localEulerAngles = new Vector3(0, 0, direction);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().caughtByGuard();
            guardCont.resetChildren();
        }

    }
}
