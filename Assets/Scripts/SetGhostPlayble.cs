using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGhostPlayble : MonoBehaviour {

    public PlayerState.GhostPlayable state;
    public GameObject ghostCButton;
    public GameObject peasantCButton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerState.ghostPlayable = state;
            if(state == PlayerState.GhostPlayable.Yes)
            {
                ghostCButton.SetActive(true);
                peasantCButton.SetActive(false);
            }
            else
            {
                ghostCButton.SetActive(false);
                peasantCButton.SetActive(false);
            }
            
        }
        
    }
}
