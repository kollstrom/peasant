using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CButton : MonoBehaviour {

    public bool ghostIsPlayable;
    public GameObject ghostCButton;
    public GameObject peasantCButton;

	// Use this for initialization
	void Start () {
        ghostCButton.SetActive(ghostIsPlayable);
        peasantCButton.SetActive(!ghostCButton.activeSelf && ghostIsPlayable);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("c"))
        {
            if(ghostCButton.activeSelf && ghostIsPlayable)
            {
                ghostCButton.SetActive(false);
                peasantCButton.SetActive(true);
            }
            else if (peasantCButton.activeSelf && ghostIsPlayable)
            {
                ghostCButton.SetActive(true);
                peasantCButton.SetActive(false);
            }

        }
	}
}
