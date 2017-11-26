using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : MonoBehaviour {

    public float moveSpeed;
    public GameObject spacebar;

    private Animator anim;
    private float step;
    private Vector3 firstPoint;
    private bool walkOnShip;

    void Start()
    {
        walkOnShip = false;
        firstPoint = transform.position + new Vector3(1.7f, 0, 0);
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (walkOnShip == true)
        {
            step = moveSpeed * Time.deltaTime;
            if (transform.position == firstPoint)
            {
                spacebar.SetActive(false);
                transform.gameObject.SetActive(false);
            }
            transform.position = Vector3.MoveTowards(transform.position, firstPoint, step);
        }
    }

    public void startWalking()
    {
        walkOnShip = true;
        anim.SetBool("isWalking", true);
    }

}
