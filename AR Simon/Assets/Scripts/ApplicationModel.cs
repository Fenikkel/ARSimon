using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationModel : MonoBehaviour {

    static public bool twoPlayers = false;

    public void ToggleMode()
    {
        twoPlayers = !twoPlayers;
    }

}
