using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.SceneManagement;
using System.Linq;


public class ButtonsScript : MonoBehaviour, IVirtualButtonEventHandler
{
    public TextMesh playerTextDebugg;
    public TextMesh simonTextDebugg;
    public TextMesh textDebugg;

    public Text roundsText;

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

    public Animator redAni, blueAni, yellowAni, greenAni;

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

    public GameObject greenAvatar;
    public GameObject blueAvatar;
    public GameObject yellowAvatar;
    public GameObject redAvatar;

    public GameObject greenImage;
    public GameObject blueImage;
    public GameObject yellowImage;
    public GameObject redImage;



    public AudioClip[] aClips;
    public AudioSource myAudioSource;

    string btnName;
    private bool gameOver;


    public float delayBetweenSimonNotes = 0.5f;
    public float delayBetweenBPressed = 0.5f;

    public float timeBetweenRounds = 1.5f;
    public float timeBetweenWinSound = 1.0f;


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

        if (!ApplicationModel.twoPlayers)
        {
            redVB.SetActive(false);
            yellowVB.SetActive(false);
            blueVB.SetActive(false);
            greenVB.SetActive(false);
        }

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

        if (ApplicationModel.traditional)
        {
            yellowAvatar.SetActive(false);
            greenAvatar.SetActive(false);
            blueAvatar.SetActive(false);
            yellowAvatar.SetActive(false);
            redAvatar.SetActive(false);

            greenImage.SetActive(false);
            blueImage.SetActive(false);
            yellowImage.SetActive(false);
            redImage.SetActive(false);
        }

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



                        if (gameStarted)
                        {

                            if (ApplicationModel.traditional)
                            {
                                PlayerMove(0);
                                PlaySound(0);
                            }
                            else
                            {
                                PlayerMove(6);
                                PlaySound(6);
                            }
                            StartCoroutine(WaitBut(0));

                        }

                        break;

                    case "sheep":


                        if (gameStarted)
                        {
                            PlaySound(6);
                            StartCoroutine(WaitBut(0));
                            PlayerMove(6);
                        }

                        break;

                    case "GreenCube":



                        if (gameStarted)
                        {
                            if (ApplicationModel.traditional)
                            {
                                PlaySound(1);
                                PlayerMove(1);

                            }
                            else
                            {
                                PlaySound(7);
                                PlayerMove(7);

                            }
                            StartCoroutine(WaitBut(1));
                        }

                        break;

                    case "Cat":



                        if (gameStarted)
                        {
                            PlayerMove(7);
                            PlaySound(7);
                            StartCoroutine(WaitBut(1));
                        }

                        break;

                    case "YellowCube":



                        if (gameStarted)
                        {
                            if (ApplicationModel.traditional)
                            {
                                PlaySound(2);
                                PlayerMove(2);

                            }
                            else
                            {
                                PlaySound(8);
                                PlayerMove(8);

                            }
                            StartCoroutine(WaitBut(2));
                        }

                        break;
                    case "LOVEDUCK":



                        if (gameStarted)
                        {
                            PlayerMove(8);
                            PlaySound(8);
                            StartCoroutine(WaitBut(2));
                        }

                        break;
                    case "BlueCube":


                        if (gameStarted)
                        {
                            if (ApplicationModel.traditional)
                            {
                                PlaySound(3);
                                PlayerMove(3);

                            }
                            else
                            {
                                PlaySound(9);
                                PlayerMove(9);

                            }
                            StartCoroutine(WaitBut(3));
                        }

                        break;
                    case "penguin":


                        if (gameStarted)
                        {
                            PlayerMove(9);
                            PlaySound(9);
                            StartCoroutine(WaitBut(3));
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

    IEnumerator WaitBut(int index)
    {
        PressButton(index);
        yield return new WaitForSeconds(delayBetweenBPressed);
        ReleaseButton(index);

    }

    IEnumerator Simon()
    {

        while (!gameOver)
        {
            //Torn de Simon
            System.Random rnd = new System.Random();
            int random = 5;

            if (ApplicationModel.traditional)
            {

                random = rnd.Next(0, 4); //el min value es inclusiu i el max value es exclusiu

            }
            else
            {
                random = rnd.Next(6, 10); //el min value es inclusiu i el max value es exclusiu

            }
            //simonList.Add(random);
            simonList = simonList.Concat(new int[] { random }).ToArray();
            //simonTextDebugg.text = "Simon: " + random;

            yield return new WaitForSeconds(timeBetweenRounds);

            //Toquem totes les notes de lallista
            foreach (int index in simonList)
            {

                PlaySound(index);

                if (ApplicationModel.traditional)
                {
                    PressButton(index);
                }
                else
                {
                    PressButton(index-6);
                }


                yield return new WaitForSeconds(delayBetweenBPressed);

                if (ApplicationModel.traditional)
                {
                    ReleaseButton(index);
                }
                else
                {
                    ReleaseButton(index - 6);
                }

                yield return new WaitForSeconds(delayBetweenSimonNotes);
            }

            //Torn del jugardor
            touchActivated = true;
            yield return new WaitUntil(() => simonList.Length == playerList.Length);

            

            if (playerMovesCounter != 0)
            {
                yield return new WaitForSeconds(timeBetweenWinSound); //CUIDAU!
                PlaySound(4);

                if (!ApplicationModel.traditional) //si no esta en modo tradicional
                {
                    AllTogether();
                }
            }


     


            round++;
            playerMovesCounter = 0;
            touchActivated = false;
            //playerList.Clear();
            playerList = new int[] { };

        }


    }

    private void AllTogether()
    {
        redAni.Play("jump");
        yellowAni.Play("jump");
        greenAni.Play("jump");
        blueAni.Play("jump");
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
            roundsText.text = "Scored: " + (round * 10);
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
        PlaySound(5);
        //StopAllCoroutines();
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
        /*
        yellowAvatar.SetActive(true);
        greenAvatar.SetActive(true);
        blueAvatar.SetActive(true);
        yellowAvatar.SetActive(true);
        redAvatar.SetActive(true);

        greenImage.SetActive(true);
        blueImage.SetActive(true);
        yellowImage.SetActive(true);
        redImage.SetActive(true);
        */
        touchActivated = true;
        gameStarted = false;
        gameOver = false;
        round = 0;
        playerMovesCounter = 0;
    }


    private void PressButton(int index)
    {
        switch(index){

            case 0://red
                if (!ApplicationModel.traditional) //si no esta en modo tradicional
                {
                    redAni.Play("jump");
                }

                redRenderer.material.color = redColorTrans;
                break;

            case 1://green
                if (!ApplicationModel.traditional)
                {
                    //greenAni.SetBool("attack", true);
                    greenAni.Play("jump");
                    //greenAni.Play("mole_attack");
                }
                greenRenderer.material.color = greenColorTrans;
                break;

            case 2://yellow
                if (!ApplicationModel.traditional)
                {
                    yellowAni.Play("jump");
                }
                yellowRenderer.material.color = yellowColorTrans;
                break;

            case 3://blue
                if (!ApplicationModel.traditional)
                {
                    blueAni.Play("jump");
                }
                blueRenderer.material.color = blueColorTrans;
                break;

            default:
                break;
        }
    }

    private void ReleaseButton(int index)
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

    private void PlaySound(int index)
    {
        //red: 0, green: 1, yellow: 2, blue: 3, win: 4, lose: 5
        if (index >= aClips.Length)
        {
            print("index: " + index + " -> out of aClips range");
        }
        else
        {
            myAudioSource.clip = aClips[index];
            myAudioSource.Play();
        }

    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        
        
        if (vb.Equals(redBehaviour))
        {

            PressButton(0);

            if (gameStarted)
            {

                if (ApplicationModel.traditional)
                {
                    PlayerMove(0);
                    PlaySound(0);
                }
                else
                {
                    PlayerMove(6);
                    PlaySound(6);
                }

            }


        }
        else if (vb.Equals(greenBehaviour))
        {

            PressButton(1);
            if (gameStarted)
            {

                if (ApplicationModel.traditional)
                {
                    PlayerMove(1);
                    PlaySound(1);
                }
                else
                {
                    PlayerMove(7);
                    PlaySound(7);
                }

            }



        }
        else if (vb.Equals(yellowBehaviour)) 
        {

            PressButton(2);
            if (gameStarted)
            {

                if (ApplicationModel.traditional)
                {
                    PlayerMove(2);
                    PlaySound(2);
                }
                else
                {
                    PlayerMove(8);
                    PlaySound(8);
                }

            }


        }
        else if (vb.Equals(blueBehaviour))
        {

            PressButton(3);
            if (gameStarted)
            {

                if (ApplicationModel.traditional)
                {
                    PlayerMove(3);
                    PlaySound(3);
                }
                else
                {
                    PlayerMove(9);
                    PlaySound(9);
                }

            }

        }

        
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {

        if (vb.Equals(redBehaviour))
        {
            ReleaseButton(0);
        }
        else if (vb.Equals(greenBehaviour))
        {
            ReleaseButton(1);

        }
        else if (vb.Equals(yellowBehaviour))
        {
            ReleaseButton(2);
        }
        else if (vb.Equals(blueBehaviour))
        {
            ReleaseButton(3);
        }
    }

    public void BackToMenu()
    {
        ApplicationModel.twoPlayers = false;
        ApplicationModel.traditional = false;
        SceneManager.LoadScene(0);

    }

}
