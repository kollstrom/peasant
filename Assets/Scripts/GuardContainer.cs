using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardContainer : MonoBehaviour {
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
            
        }
    }
}
