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
    public float fadeTime;

    // Music
    public AudioSource outsideTheme;
    public AudioSource dungeonTheme;

    public AudioSource GetCurrentAudioSource()
    {
        if (outsideTheme.isPlaying)
        {
            return outsideTheme;
        }
        else 
        {
            return dungeonTheme;
        }
    }

    public void StartStopMusic(AudioSource shouldStart, AudioSource shouldStop)
    {
        if (shouldStop.isPlaying)
        {
            shouldStop.Stop();
        }
        if (!shouldStart.isPlaying)
        {
            shouldStart.Play();
        }
    }

    public void PlayMusic(string name)
    {
        if (name == "InsideMusicZone")
        {
            StartStopMusic(dungeonTheme, outsideTheme);
        }
        else if (name == "OutsideMusicZone")
        {
            StartStopMusic(outsideTheme, dungeonTheme);
        }
    }
}
