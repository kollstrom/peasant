using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPlayer : MonoBehaviour {

    private GuardContainer guardCont;
    private BoxCollider2D box2d;
    public LayerMask layermask;
    private bool startCatchTimer;
    private float timer;
    private SoundManager sfxManager;

    // Use this for initialization
    void Start () {
        guardCont = transform.parent.parent.gameObject.GetComponent<GuardContainer>();
        box2d = transform.gameObject.GetComponent<BoxCollider2D>();
        startCatchTimer = false;
        sfxManager = FindObjectOfType<SoundManager>();
        timer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 1000f, layermask);

        if (hit.collider != null)
        {
            box2d.offset = new Vector2(box2d.offset.x, -hit.distance/2);
            box2d.size = new Vector2(box2d.size.x, hit.distance);
        }

        if (startCatchTimer)
        {
            timer += Time.deltaTime;
            if (timer >= 2 )
            {
                guardCont.resetChildren();
                FindObjectOfType<PlayerController>().caughtByGuard();
                startCatchTimer = false;
                timer = 0;

            }
        }

    }


    public void turn(float direction)
    {
        transform.localEulerAngles = new Vector3(0, 0, direction);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerState.canCatch == PlayerState.GuardCanCatch.Yes)
        {
            sfxManager.heySound.Play();
            StartCoroutine(PlayFailSound());
            guardCont.catchingPlayer(transform.parent.gameObject);
            FindObjectOfType<CameraController>().setCameraToGameObject(transform.parent.gameObject);
            collision.GetComponent<PlayerController>().disable();
            startCatchTimer = true;
            PlayerState.canCatch = PlayerState.GuardCanCatch.No;
        }
    }

    private IEnumerator PlayFailSound ()
    {
        yield return new WaitForSecondsRealtime(1);
        sfxManager.caughtByGuardSound.Play();
        PlayerState.catched = PlayerState.Catched.Yes;
    }
}
