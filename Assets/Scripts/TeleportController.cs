using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour {

    public Vector2 teleportPosition;
    public CameraController camController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.position = teleportPosition;
            camController.setCameraPositionToVector(teleportPosition);
            collision.GetComponent<PlayerController>().respawnPosition = transform.position - new Vector3(0, 1, 0);

            if (gameObject.name =="Teleporter(into dungeon)")
            {
                SoundManager.location = SoundManager.Location.Inside;
            }
            else if (gameObject.name == "Teleporter (out of dungeon)")
            {
                SoundManager.location = SoundManager.Location.Outside;
            }
        }
    }
}
