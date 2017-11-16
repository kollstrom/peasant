using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPlayer : MonoBehaviour {

    private GuardContainer guardCont;
    private BoxCollider2D box2d;
    public LayerMask layermask;

    // Use this for initialization
    void Start () {
        guardCont = transform.parent.parent.gameObject.GetComponent<GuardContainer>();
        box2d = transform.gameObject.GetComponent<BoxCollider2D>();
}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 1000f, layermask);

        if (hit.collider != null)
        {
            box2d.offset = new Vector2(box2d.offset.x, -hit.distance/2);
            box2d.size = new Vector2(box2d.size.x, hit.distance);
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
