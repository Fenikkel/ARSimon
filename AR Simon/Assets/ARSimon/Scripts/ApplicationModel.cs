using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationModel : MonoBehaviour {

    static public bool twoPlayers = false;
    static public bool traditional = false;
    public UIAudioManager script;

    public void TogglePlayMode()
    {
        twoPlayers = !twoPlayers;

        if (twoPlayers)
        {
            script.PlayToggleOn();
        }
        else
        {
            script.PlayToggleOff();

        }

    }

    public void ToggleTraditional()
    {
        traditional = !traditional;

        if (traditional)
        {
            script.PlayToggleOn();
        }
        else
        {
            script.PlayToggleOff();

        }
    }

}
