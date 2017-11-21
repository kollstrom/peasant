using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {

    public float moveSpeed;

    private bool sailAvay;
    private float step;
    private Vector3 firstPoint;
    private Vector3 secondPoint;
    private Vector3 currentPoint;
    private bool isShown = false;

    // Use this for initialization
    void Start () {
        sailAvay = false;
        step = moveSpeed * Time.deltaTime;
        firstPoint = transform.position + new Vector3(0, -25, 0);
        secondPoint = firstPoint + new Vector3(0, -15, 0);
        currentPoint = firstPoint;
    }
	
	// Update is called once per frame
	void Update () {
        if (sailAvay == true)
        {
            if (transform.position == firstPoint)
            {
                currentPoint = secondPoint;
                FindObjectOfType<CameraController>().lerpToPosition(firstPoint);
            }
            else if (transform.position == secondPoint && !isShown)
            {
                isShown = true;
                FindObjectOfType<OutroTrigger>().showDialog(true);
            }
            transform.position = Vector3.MoveTowards(transform.position, currentPoint, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FindObjectOfType<PlayerController>().disable();
            FindObjectOfType<PlayerController>().gameObject.SetActive(false);
            FindObjectOfType<CameraController>().setCameraToGameObject(this.gameObject);
            sailAvay = true;
        }
    }
}
