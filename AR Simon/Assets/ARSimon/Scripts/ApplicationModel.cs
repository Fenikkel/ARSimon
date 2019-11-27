using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationModel : MonoBehaviour {

    static public bool twoPlayers = false;
    static public bool traditional = false;

    public void TogglePlayMode()
    {
        twoPlayers = !twoPlayers;
    }

    public void ToggleTraditional()
    {
        traditional = !traditional;
    }

}
