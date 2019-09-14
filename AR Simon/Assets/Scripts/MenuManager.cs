using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public void TwoPlayers(bool active)
    {
        ApplicationModel.twoPlayers = active;
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(1);
    }
}
