using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicZone : MonoBehaviour {

    SoundManager soundManager;
	void Start () {
        soundManager = GetComponentInParent<SoundManager>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            soundManager.PlayMusic(this.name);
        }
    }

}
