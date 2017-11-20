using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    public enum playerState
    {
        Ghost, Player
    }

    public static playerState state = playerState.Player;

    public enum LunchState
    {
        PickedUp, NotPickedUp
    }

    public static LunchState lunchState = LunchState.NotPickedUp;

    public enum SavedState
    {
        Saved, NotSaved
    }

    public static SavedState savedState = SavedState.NotSaved;

    public enum GhostPlayable
    {
        Yes, No
    }

    public static GhostPlayable ghostPlayable = GhostPlayable.No;

    public enum GuardCanCatch
    {
        Yes, No
    }

    public static GuardCanCatch canCatch = GuardCanCatch.Yes;

    public enum Catched
    {
        Yes, No
    }

    public static Catched catched = Catched.No;
}
