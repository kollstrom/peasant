using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CButton : MonoBehaviour {

    public GameObject ghostCButton;
    public GameObject peasantCButton;

	// Use this for initialization
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
            if (Input.GetKeyDown("c"))
            {
                if(ghostCButton.activeSelf && PlayerState.ghostPlayable == PlayerState.GhostPlayable.Yes)
                {
                    ghostCButton.SetActive(false);
                    peasantCButton.SetActive(true);
                }
                else if (peasantCButton.activeSelf && PlayerState.ghostPlayable == PlayerState.GhostPlayable.Yes)
                {
                    ghostCButton.SetActive(true);
                    peasantCButton.SetActive(false);
                }

        }
	}
}
