using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ButtonsScript : MonoBehaviour, IVirtualButtonEventHandler
{

    private GameObject redVB;
    private GameObject redCube;
    
    void Start () {
        redVB = GameObject.Find("RedButton");
        redCube = GameObject.Find("RedCube");
        redVB.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

    }


    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        //throw new System.NotImplementedException();
        redCube.SetActive(false);
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        //throw new System.NotImplementedException();
        redCube.SetActive(true);

    }
}
