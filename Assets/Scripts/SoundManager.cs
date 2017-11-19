using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    // Sound Effects
    public AudioSource nextSound;
    public AudioSource ghostSound;
    public AudioSource obtainedLunchSound;
    public AudioSource swapToGhostSound;
    public AudioSource swapToWomanSound;
    public AudioSource checkpointSound;
    public AudioSource caughtByGuardSound;
    public AudioSource closeDoorSound;
    public AudioSource openDoorSound;
    public AudioSource heySound;
    public AudioSource celebrationSound;

    // Music
    public AudioSource outsideTheme;
    public AudioSource dungeonTheme;

    public static Location location = Location.Outside;
    private Location previousLocation;

    public enum Location 
    {
        Inside, Outside
    }

	void Start () {
        previousLocation = location;
        outsideTheme.Play();
	}
	
	void Update () {
        if (previousLocation != location)
        {
            if (location == Location.Inside)
            {
                outsideTheme.Stop();
                dungeonTheme.Play();
            }
            else if (location == Location.Outside)
            {
                dungeonTheme.Stop();
                outsideTheme.Play();
            }
        }
        previousLocation = location;
	}
}
