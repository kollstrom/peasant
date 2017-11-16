using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredByGhost : MonoBehaviour {

    private GameObject spaceButtonImage;
    private bool ghostInTrigger = false;
    private Vector2 ghostPosition;
    private GameObject parent;
    private MovingGuard parentScriptMovingGuard;
    private StandingGuard parentScriptStandingGuard;
    private SoundEffectsManager sfxManager;

    // Use this for initialization
    void Start () {
        spaceButtonImage = GameObject.Find("/UI/Canvas/SpaceButton");
        spaceButtonImage.SetActive(false);
        parent = transform.parent.gameObject;
        sfxManager = FindObjectOfType<SoundEffectsManager>();

        if(parent.tag == "MovingGuard")
        {
            parentScriptMovingGuard = parent.GetComponent<MovingGuard>();
        }else if(parent.tag == "StandingGuard"){
            parentScriptStandingGuard = parent.GetComponent<StandingGuard>();
        }
    }
	
	// Update is called once per frame
	void Update () {

        switch (parent.tag)
        {
            case "MovingGuard":
                if (ghostInTrigger && Input.GetKeyDown("space") && parentScriptMovingGuard.state == MovingGuard.GuardState.Patrolling)
                {
                    ghostInTrigger = false;
                    spaceButtonImage.SetActive(false);
                    sfxManager.ghostSound.Play();
                    parentScriptMovingGuard.scared(ghostPosition);
                    parentScriptMovingGuard.lastScarePoint = transform.parent.position;
                }
                break;
            case "StandingGuard":
                if(ghostInTrigger && Input.GetKeyDown("space") && parentScriptStandingGuard.state == StandingGuard.GuardState.Standing)
                {
                    ghostInTrigger = false;
                    spaceButtonImage.SetActive(false);
                    parentScriptStandingGuard.scared(ghostPosition);
                }
                break;
        }
        

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ghost" && parent.tag == "MovingGuard" && parentScriptMovingGuard.state == MovingGuard.GuardState.Patrolling)
        {
            spaceButtonImage.SetActive(true);
            ghostInTrigger = true;
            ghostPosition = collision.transform.position;

        }
        else if (collision.gameObject.tag == "Ghost" && parent.tag == "StandingGuard" && parentScriptStandingGuard.state == StandingGuard.GuardState.Standing)
        {
            spaceButtonImage.SetActive(true);
            ghostInTrigger = true;
            ghostPosition = collision.transform.position;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            spaceButtonImage.SetActive(false);
            ghostInTrigger = false;

        }
    }
}
