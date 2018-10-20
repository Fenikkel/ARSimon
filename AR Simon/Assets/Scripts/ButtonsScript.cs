using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ButtonsScript : MonoBehaviour, IVirtualButtonEventHandler
{

    private GameObject redVB, blueVB, greenVB, yellowVB;
    private GameObject redCube, yellowCube, greenCube, blueCube;

    private VirtualButtonBehaviour redBehaviour;
    private VirtualButtonBehaviour yellowBehaviour;
    private VirtualButtonBehaviour blueBehaviour;
    private VirtualButtonBehaviour greenBehaviour;

    private Color redColor;
    private Color yellowColor;
    private Color greenColor;
    private Color blueColor;

    private Color redColorTrans;
    private Color yellowColorTrans;
    private Color greenColorTrans;
    private Color blueColorTrans;

    private Renderer redRenderer;
    private Renderer yellowRenderer;
    private Renderer blueRenderer;
    private Renderer greenRenderer;



    public AudioClip[] aClips;
    public AudioSource myAudioSource;

    void Start () {
        redVB = GameObject.Find("RedButton");
        yellowVB = GameObject.Find("YellowButton");
        blueVB = GameObject.Find("BlueButton");
        greenVB = GameObject.Find("GreenButton");
        redCube = GameObject.Find("RedCube");
        yellowCube = GameObject.Find("YellowCube");
        blueCube = GameObject.Find("BLueCube");
        greenCube = GameObject.Find("GreenCube");

        redBehaviour = redVB.GetComponent<VirtualButtonBehaviour>();
        yellowBehaviour = yellowVB.GetComponent<VirtualButtonBehaviour>();
        blueBehaviour = blueVB.GetComponent<VirtualButtonBehaviour>();
        greenBehaviour = greenVB.GetComponent<VirtualButtonBehaviour>();


        redBehaviour.RegisterEventHandler(this);
        yellowBehaviour.RegisterEventHandler(this);
        blueBehaviour.RegisterEventHandler(this);
        greenBehaviour.RegisterEventHandler(this);

        redRenderer = redCube.GetComponent<Renderer>();
        yellowRenderer = yellowCube.GetComponent<Renderer>();
        blueRenderer = blueCube.GetComponent<Renderer>();
        greenRenderer = greenCube.GetComponent<Renderer>();


        redColor = new Color(208.0f / 255.0f, 20.0f / 255.0f, 20.0f / 255.0f, 1.0f);//redRenderer.material.color;
        redColorTrans = new Color(208.0f / 255.0f, 20.0f / 255.0f, 20.0f / 255.0f, 0.5f);//redRenderer.material.color;
        //redColorTrans.a = 0.7f;

        yellowColor = yellowRenderer.material.color;
        yellowColorTrans = yellowRenderer.material.color;
        yellowColorTrans.a = 0.7f;

        greenColor = greenRenderer.material.color;
        blueColor = blueRenderer.material.color;




        //redVB.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        //yellowVB.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        //blueVB.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        //greenVB.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

        myAudioSource = GetComponent<AudioSource>();

    }


    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        //throw new System.NotImplementedException();

        if (vb.Equals(redBehaviour)) //(redVB.GetComponent<VirtualButtonBehaviour>())
        {

            //redCube.SetActive(false);
            myAudioSource.clip = aClips[0];
            myAudioSource.Play();
            /* redCube.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);*/
            //SoundAndColor("red");
            //redColor.a = 0.5f;
            redRenderer.material.color = redColorTrans;
            


        }
        else if (vb.Equals(yellowBehaviour)) //yellowVB.GetComponent<VirtualButtonBehaviour>()
        {

            //yellowCube.SetActive(false);
            myAudioSource.clip = aClips[2];
            myAudioSource.Play();
            /*yellowCube.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);*/
            //SoundAndColor("yellow");
            //yellowColor.a = 0.5f;
            yellowRenderer.material.color = yellowColorTrans;


        }
        else if(vb.Equals(blueBehaviour))//blueVB.GetComponent<VirtualButtonBehaviour>()
        {
            //SoundAndColor("blue");
            //blueCube.SetActive(false);
            myAudioSource.clip = aClips[3];
             myAudioSource.Play();
            blueRenderer.material.color = blueColorTrans;

            /*  blueCube.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);*/
            //blueColor.a = 0.5f;


        }
        else if (vb.Equals(greenBehaviour))//if (vb.Equals(greenVB.GetComponent<VirtualButtonBehaviour>()))
        {
            //SoundAndColor("green");
            //greenCube.SetActive(false);
            myAudioSource.clip = aClips[1];
            myAudioSource.Play();
            greenRenderer.material.color = greenColorTrans;

            /*greenCube.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);*/
            //greenColor.a = 0.5f;



        }

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {

        //redRenderer.material.color = redColor;

        //yellowRenderer.material.color = yellowColor;

        /*redCube.SetActive(true);
        greenCube.SetActive(true);
        blueCube.SetActive(true);
        yellowCube.SetActive(true);
        */
        //redCube.GetComponent<Renderer>().material.color = Color(1, 208.0f/255.0f, 20.0f / 255.0f, 20.0f / 255.0f);

        if (vb.Equals(redBehaviour))
        {
            redCube.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else if (vb.Equals(yellowBehaviour))
        {
            yellowCube.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);

        }
        else if (vb.Equals(blueBehaviour))//blueVB.GetComponent<VirtualButtonBehaviour>()
        {
            blueCube.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

        }
        else if (vb.Equals(greenBehaviour))
        {
            greenCube.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

        }





    }
    /*
    IEnumerator SoundAndColor(string color)
    {
        switch (color)
        {
            case "red":
                myAudioSource.clip = aClips[0];
                myAudioSource.Play();
                redCube.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                break;
            case "yellow":
                myAudioSource.clip = aClips[2];
                myAudioSource.Play();
                yellowCube.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                break;
            case "blue":
                myAudioSource.clip = aClips[3];
                myAudioSource.Play();
                blueCube.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                break;
            case "green":
                myAudioSource.clip = aClips[1];
                myAudioSource.Play();
                greenCube.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                break;
            default:
                break;
        }

        yield return null;
    }
    */
}
