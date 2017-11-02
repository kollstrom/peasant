using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public GameObject ghost;
    public float moveSpeed;

    private Vector3 targetPos;

	void LateUpdate () {
        if(PlayerState.state == PlayerState.playerState.Ghost)
        {
            targetPos = new Vector3(ghost.transform.position.x, ghost.transform.position.y, transform.position.z);
            
        }
        else
        {
            targetPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
        
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
	}

    public void setCameraPosition(Vector2 v2)
    {
        transform.position = new Vector3(v2.x, v2.y, transform.position.z);
    }
}
