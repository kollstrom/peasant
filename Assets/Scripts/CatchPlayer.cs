using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPlayer : MonoBehaviour {

    private GuardContainer guardCont;

	// Use this for initialization
	void Start () {
        guardCont = transform.parent.parent.gameObject.GetComponent<GuardContainer>();
    }
	
	// Update is called once per frame
	void Update () {
		
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
