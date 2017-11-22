using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boat : MonoBehaviour {

    public float moveSpeed;

    private bool sailAway;
    private float step;
    private Vector3 firstPoint;
    private Vector3 secondPoint;
    private Vector3 thirdPoint;
    private Vector3 currentPoint;
    private bool isShown = false;

    public float x1;
    public float x2;
    public float x3;

    private float timer = 0;

    // Use this for initialization
    void Start () {
        sailAway = false;
        step = moveSpeed * Time.deltaTime;

        firstPoint = transform.position + new Vector3(x1, 0, 0);
        secondPoint = firstPoint + new Vector3(x2, 0, 0);
        thirdPoint = secondPoint + new Vector3(x3, 0, 0);
        currentPoint = firstPoint;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (sailAway == true)
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
                currentPoint = thirdPoint;
            }
            else if(transform.position == thirdPoint)
            {
                PlayerState.catched = PlayerState.Catched.Yes;
                timer += Time.deltaTime;
                if(timer >= 1)
                {
                    SceneManager.LoadSceneAsync("Credits");
                }

                
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
            sailAway = true;
        }
    }
}
