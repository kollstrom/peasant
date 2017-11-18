using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public GameObject ghost;
    public float moveSpeed;

    private Vector3 targetPos;
    private GameObject following;

    private void Start()
    {
        following = player;
    }

    void LateUpdate () {
        targetPos = new Vector3(following.transform.position.x, following.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
	}

    public void setCameraPositionToVector(Vector2 v2)
    {
        transform.position = new Vector3(v2.x, v2.y, transform.position.z);
    }

    public void setCameraToGameObject(GameObject go)
    {
        following = go;
    }
}
