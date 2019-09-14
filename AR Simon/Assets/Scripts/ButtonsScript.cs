using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;
using System.Linq;


public class ButtonsScript : MonoBehaviour, IVirtualButtonEventHandler
{
    public TextMesh playerTextDebugg;
    public TextMesh simonTextDebugg;
    public TextMesh textDebugg;

    public GameObject endgameCanvas;

    private IEnumerator coroutine;

    private bool touchActivated = true;
    private bool gameStarted = false;

    //List<int> simonList = new List<int>();
    //List<int> playerList = new List<int>();
    //ArrayList simonList = new ArrayList();
    //ArrayList playerList = new ArrayList();
    int[] simonList = new int[] {};
    int[] playerList = new int[] { };

    private int round = 0; //rondas superadas
    private int playerMovesCounter = 0;


    private GameObject redVB, blueVB, greenVB, yellowVB;
    private GameObject redCube, yellowCube, greenCube, blueCube;
    private GameObject textButtonStart, buttonStart;

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

    string btnName;
    private bool gameOver;


    public float delayBetweenSimonNotes = 0.5f;
    public float delayBetweenBPressed = 0.5f;

    public float timeBetweenRounds = 1.5f;

    //public float timeBetween = 1.0f;


    void Start () {

        coroutine = Simon();

        //playerList.Clear();
        //simonList.Clear();

        playerList = new int[] { };
        simonList = new int[] { };

        touchActivated = true;
        gameStarted = false;
        gameOver = false;
        round = 0;
        playerMovesCounter = 0;

        redVB = GameObject.Find("RedButton");
        yellowVB = GameObject.Find("YellowButton");
        blueVB = GameObject.Find("BleuButton"); //recorda que se diu "Bleu"
        greenVB = GameObject.Find("GreenButton");
        redCube = GameObject.Find("RedCube");
        yellowCube = GameObject.Find("YellowCube");
        blueCube = GameObject.Find("BlueCube");
        greenCube = GameObject.Find("GreenCube");

        textButtonStart = GameObject.Find("StartText");
        buttonStart = GameObject.Find("StartButton");

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


        redColor = new Color(208.0f / 255.0f, 20.0f / 255.0f, 20.0f / 255.0f, 1.0f);//forma cutre
        redColorTrans = new Color(208.0f / 255.0f, 20.0f / 255.0f, 20.0f / 255.0f, 0.4f);

        yellowColor = yellowRenderer.material.color;
        yellowColorTrans = yellowRenderer.material.color;
        yellowColorTrans.a = 0.4f;

        greenColor = greenRenderer.material.color;
        greenColorTrans = greenRenderer.material.color;
        greenColorTrans.a = 0.4f;


        blueColor = blueRenderer.material.color;
        blueColorTrans = blueRenderer.material.color;
        blueColorTrans.a = 0.4f;

        myAudioSource = GetComponent<AudioSource>();

    }

    void Update() {
        if (touchActivated && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit))
            {
                btnName = Hit.transform.name;
                switch (btnName)
                {
                    case "StartButton":
                        StartGame();
                        break;

                    case "StartText":
                        StartGame();
                        break;

                    case "RedCube":

                        myAudioSource.clip = aClips[0];
                        myAudioSource.Play();

                        if (gameStarted)
                        {
                            PlayerMove(0);
                        }


                        break;

                    case "GreenCube":

                        myAudioSource.clip = aClips[1];
                        myAudioSource.Play();

                        if (gameStarted)
                        {
                            PlayerMove(1);
                        }

                        break;

                    case "YellowCube":

                        myAudioSource.clip = aClips[2];
                        myAudioSource.Play();

                        if (gameStarted)
                        {
                            PlayerMove(2);
                        }


                        break;
                    case "BlueCube":

                        myAudioSource.clip = aClips[3];
                        myAudioSource.Play();

                        if (gameStarted)
                        {
                            PlayerMove(3);
                        }


                        break;
                    default:
                        break;
                }
            }
        }

    }

    private void StartGame()
    {

        textButtonStart.SetActive(false);
        buttonStart.SetActive(false);
        touchActivated = false;
        gameStarted = true;

        StartCoroutine(coroutine);
    }

    IEnumerator Simon()
    {

        while (!gameOver)
        {
            //Torn de Simon
            System.Random rnd = new System.Random();
            int random = rnd.Next(0, 4); //el min value es inclusiu i el max value es exclusiu
            //simonList.Add(random);
            simonList = simonList.Concat(new int[] { random }).ToArray();
            //simonTextDebugg.text = "Simon: " + random;

            yield return new WaitForSeconds(timeBetweenRounds);

            //Toquem totes les notes de lallista
            foreach (int index in simonList)
            {
                //myAudioSource.clip = aClips[sound];
                //myAudioSource.Play();
                PressButton(index);
                PlaySound(index);

                yield return new WaitForSeconds(delayBetweenBPressed);

                ReleaseButton(index);

                yield return new WaitForSeconds(delayBetweenSimonNotes);
            }

            //Torn del jugardor
            touchActivated = true;
            yield return new WaitUntil(() => simonList.Length == playerList.Length);
            //yield return new WaitUntil(() => simonList.Count == playerList.Count);

            round++;
            playerMovesCounter = 0;
            touchActivated = false;
            //playerList.Clear();
            playerList = new int[] { };

        }


    }


    private void PlayerMove(int move)
    {
        //playerList.Add(move);
        playerList = playerList.Concat(new int[] { move }).ToArray();
        //playerTextDebugg.text = "Simon: " + move;
        playerMovesCounter++;
        simonTextDebugg.text = "SimonQuetoca: " + simonList[playerMovesCounter - 1];
        playerTextDebugg.text = "PlayerHatocado: " + playerList[playerMovesCounter - 1];

        if ( ! ( simonList[playerMovesCounter - 1].Equals( playerList[playerMovesCounter - 1] ) )) //analitzar en debug text
        {
            textDebugg.text= "MAL";
            GameOver();
        }
        textDebugg.text = "BE";
        //textDebugg.text = " " + (int)simonList[playerMovesCounter - 1];
        //textDebugg.text = simonList.;
        //HAY QUE OBTENER EL VALOR DE SIMON LIST CON UN ITERADOR?? y el de player mirar su last??

    }

    private void GameOver()
    {
        print("GameOver");
        ResetGame();
        endgameCanvas.SetActive(true);
       
        //SceneManager.LoadScene(2);
    }

    private void ResetGame()
    {
        StopCoroutine(coroutine);
        textButtonStart.SetActive(true);
        buttonStart.SetActive(true);
        playerList = new int[] { };
        simonList = new int[] { };

        touchActivated = true;
        gameStarted = false;
        gameOver = false;
        round = 0;
        playerMovesCounter = 0;
    }


    public void PressButton(int index)
    {
        switch(index){

            case 0://red

                redRenderer.material.color = redColorTrans;
                break;

            case 1://green

                greenRenderer.material.color = greenColorTrans;
                break;

            case 2://yellow

                yellowRenderer.material.color = yellowColorTrans;
                break;

            case 3://blue

                blueRenderer.material.color = blueColorTrans;
                break;

            default:
                break;
        }
    }

    public void ReleaseButton(int index)
    {
        switch (index)
        {

            case 0://red

                redRenderer.material.color = redColor;
                break;

            case 1://green

                greenRenderer.material.color = greenColor;
                break;

            case 2://yellow

                yellowRenderer.material.color = yellowColor;
                break;

            case 3://blue

                blueRenderer.material.color = blueColor;
                break;

            default:
                break;
        }
    }

    public void PlaySound(int index)
    {
        switch (index)
        {

            case 0://red
                myAudioSource.clip = aClips[0];
                myAudioSource.Play();
                break;

            case 1://green

                myAudioSource.clip = aClips[1];
                myAudioSource.Play();
                break;

            case 2://yellow

                myAudioSource.clip = aClips[2];
                myAudioSource.Play();
                break;

            case 3://blue

                myAudioSource.clip = aClips[3];
                myAudioSource.Play();
                break;

            default:
                break;
        }
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        
        /*
        if (vb.Equals(redBehaviour))
        {

            myAudioSource.clip = aClips[0];
            myAudioSource.Play();
            redRenderer.material.color = redColorTrans;



        }
        else if (vb.Equals(yellowBehaviour)) 
        {

            myAudioSource.clip = aClips[2];
            myAudioSource.Play();
            yellowRenderer.material.color = yellowColorTrans;


        }
        else if (vb.Equals(blueBehaviour))
        {

            myAudioSource.clip = aClips[3];
            myAudioSource.Play();
            blueRenderer.material.color = blueColorTrans;

        }
        else if (vb.Equals(greenBehaviour))
        {

            myAudioSource.clip = aClips[1];
            myAudioSource.Play();
            greenRenderer.material.color = greenColorTrans;

        }
        */
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        /*
        if (vb.Equals(redBehaviour))
        {
            redRenderer.material.color = redColor;
        }
        else if (vb.Equals(yellowBehaviour))
        {
            yellowRenderer.material.color = yellowColor;

        }
        else if (vb.Equals(blueBehaviour))
        {
            blueRenderer.material.color = blueColor;
        }
        else if (vb.Equals(greenBehaviour))
        {
            greenRenderer.material.color = greenColor;
        }
        */
    }
   
}
