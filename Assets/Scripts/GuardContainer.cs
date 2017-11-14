using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardContainer : MonoBehaviour {
    public void resetChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<MovingGuard>().resetGuard();
        }
    }
}
