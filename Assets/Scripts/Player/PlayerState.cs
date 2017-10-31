using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    public enum playerState
    {
        Ghost, Player
    }

    public static playerState state = playerState.Player;
}
