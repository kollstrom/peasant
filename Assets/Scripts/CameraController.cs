using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public GameObject ghost;
    public float moveSpeed;

    private GameObject following;
    private Vector2 focusPosition;
    [HideInInspector]
    private bool shouldFollow;
    private GameObject lunch;

    private void Start()
    {
        shouldFollow = true;
        following = player;
        lunch = transform.GetChild(0).gameObject;
    }

    void LateUpdate () {
        if (shouldFollow)
        {
            lerp(new Vector2(following.transform.position.x, following.transform.position.y));
        }
        else
        {
            lerp(focusPosition);
        }
        
	}

    private void lerp(Vector2 v2)
    {
        Vector3 targetPos = new Vector3(v2.x, v2.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    public void setCameraPositionToVector(Vector2 v2)
    {
        transform.position = new Vector3(v2.x, v2.y, transform.position.z);
    }

    public void lerpToPosition(Vector2 v2)
    {
        shouldFollow = false;
        focusPosition = v2;

    }

    public void setCameraToGameObject(GameObject go)
    {
        following = go;
    }

    public void showHideLunch(bool b)
    {
        lunch.SetActive(b);
    }
}
