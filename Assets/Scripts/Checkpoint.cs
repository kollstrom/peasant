using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private SoundEffectsManager sfxManager;

    private bool visitedBefore = false;

    private void Start()
    {
        sfxManager = FindObjectOfType<SoundEffectsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!visitedBefore)
            {
                sfxManager.checkpointSound.Play();
            }
            visitedBefore = true;
            collision.GetComponent<PlayerController>().respawnPosition = collision.GetComponent<PlayerController>().transform.position;
        }
    }
}
