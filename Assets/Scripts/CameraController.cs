using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public GameObject ghost;
    private Vector3 targetPos;
    public float moveSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if(PlayerState.state == PlayerState.playerState.Player)
        {
            targetPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
        else
        {
            targetPos = new Vector3(ghost.transform.position.x, ghost.transform.position.y, transform.position.z);
        }
        
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
	}
}
