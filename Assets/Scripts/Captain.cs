using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : MonoBehaviour {

    public float moveSpeed;

    private float step;
    private Vector3 firstPoint;
    private bool walkOnShip;


    void Start()
    {
        walkOnShip = false;
        step = moveSpeed * Time.deltaTime;
        firstPoint = transform.position + new Vector3(1.7f, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (walkOnShip == true)
        {
            if (transform.position == firstPoint)
            {
                transform.gameObject.SetActive(false);
            }
            transform.position = Vector3.MoveTowards(transform.position, firstPoint, step);
        }
    }

    public void startWalking()
    {
        walkOnShip = true;
    }

}
