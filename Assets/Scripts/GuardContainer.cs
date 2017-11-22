using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardContainer : MonoBehaviour {

    private float timer = 0;
    private bool runTimer = true;

    private void Update()
    {
        if (runTimer)
        {
            timer += Time.deltaTime;
            if(timer >= 2)
            {
                resetChildren();
                runTimer = false;
            }
        }
    }

    public void resetChildren()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "MovingGuard")
            {
                child.gameObject.GetComponent<MovingGuard>().resetGuard();
            }
            else if (child.tag == "RotatingGuard")
            {
                child.gameObject.GetComponent<RotatingGuard>().resetGuard();
            }
            else if (child.tag == "StandingGuard")
            {
                child.gameObject.GetComponent<StandingGuard>().resetGuard();
            }

        }
    }

    public void catchingPlayer(GameObject go)
    {
        if (go.tag == "MovingGuard")
        {
            go.GetComponent<MovingGuard>().catchingPlayer = true;
        }
        else if (go.tag == "RotatingGuard")
        {
            go.GetComponent<RotatingGuard>().catchingPlayer = true;
        }
        else if (go.tag == "StandingGuard")
        {
            go.GetComponent<StandingGuard>().catchingPlayer = true;
        }

    }
}
