using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ButtonsScript : MonoBehaviour, IVirtualButtonEventHandler
{

    private bool touchActivated = true;

    //private string[] simonArray = new string[2];
    List<int> simonList = new List<int>();
    List<int> playerList = new List<int>();

    Queue simonQ = new Queue();



    private int contador = 0; //rondas superadas +1


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
    bool gameOver;

    private float playSoundTimer;
    public float timeBetweenSounds = 2.0f;

    private float simonWaitTimer;
    public float SimonTimeBetween = 1.5f;

    private float timer;
    public float timeBetween = 1.0f;


    void Start () {

        playerList.Clear();
        simonList.Clear();

        simonList.Add(0);
        simonList.Add(2);

        simonList.Add(1);
        simonList.Add(1);
        simonList.Add(1);
        simonList.Add(1);
        simonList.Add(1);
        simonList.Add(1);

        simonQ.Enqueue(1);
        simonQ.Enqueue(1);
        simonQ.Enqueue(1);
        simonQ.Enqueue(1);
        simonQ.Enqueue(1);
        simonQ.Enqueue(1);
        simonQ.Enqueue(1);




        playSoundTimer = timeBetweenSounds;
        timer = timeBetween;
        simonWaitTimer = SimonTimeBetween;
        //DEBUGGING
        //SimonTurn();
        //DEBUGGING




        touchActivated = true;
        gameOver = false;

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


        redColor = new Color(208.0f / 255.0f, 20.0f / 255.0f, 20.0f / 255.0f, 1.0f);//redRenderer.material.color;
        redColorTrans = new Color(208.0f / 255.0f, 20.0f / 255.0f, 20.0f / 255.0f, 0.4f);//redRenderer.material.color;
        //redColorTrans.a = 0.7f;

        yellowColor = yellowRenderer.material.color;
        yellowColorTrans = yellowRenderer.material.color;
        yellowColorTrans.a = 0.4f;

        greenColor = greenRenderer.material.color;
        greenColorTrans = greenRenderer.material.color;
        greenColorTrans.a = 0.4f;


        blueColor = blueRenderer.material.color;
        blueColorTrans = blueRenderer.material.color;
        blueColorTrans.a = 0.4f;




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

            //greenCube.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
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
            //redCube.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            redRenderer.material.color = redColor;
        }
        else if (vb.Equals(yellowBehaviour))
        {
            //yellowCube.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
            yellowRenderer.material.color = yellowColor;

        }
        else if (vb.Equals(blueBehaviour))//blueVB.GetComponent<VirtualButtonBehaviour>()
        {
            blueRenderer.material.color = blueColor;
        }
        else if (vb.Equals(greenBehaviour))
        {
            greenRenderer.material.color = greenColor;
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
                        //StartCoroutine(StartSimon());
                        break;

                    case "StartText":
                        StartGame();
                        //StartCoroutine(StartSimon());
                        break;


                    case "GreenCube":
                        PlayerMove(0);
                        myAudioSource.clip = aClips[1];
                        myAudioSource.Play();
                        break;

                    case "RedCube":
                        PlayerMove(1);
                        myAudioSource.clip = aClips[0];
                        myAudioSource.Play();

                        break;

                    case "YellowCube":
                        PlayerMove(2);

                        myAudioSource.clip = aClips[2];
                        myAudioSource.Play();
                        break;
                    case "BlueCube":
                        PlayerMove(3);

                        myAudioSource.clip = aClips[3];
                        myAudioSource.Play();
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
        //SimonTurn();
        //StartCoroutine(STurn());
        //deffug();
        StartCoroutine(PlayList());

    }

    private void Deffug() // funciona fatal
    {
        while (true)
        {
            while (playSoundTimer > 0.0f)
            {
                playSoundTimer -= Time.deltaTime;
            }
            playSoundTimer = timeBetweenSounds;
            myAudioSource.clip = aClips[0];
            myAudioSource.Play();
        }
    }
    IEnumerator STurn()
    {
        touchActivated = false;
        yield return new WaitForSeconds(2);

        System.Random rnd = new System.Random();
        int random = rnd.Next(0, 3);
        simonList.Add(random);

        StartCoroutine(PlayList());

    }

    IEnumerator PlayList()
    {

        /*for (int tocados = 0; tocados < simonQ.Count; tocados++)
        {
            myAudioSource.clip = aClips[simonQ.];
            myAudioSource.Play();

            yield return new WaitForSeconds(1);

            print("Ha sonado \n");
        }*/
        //StartCoroutine(STurn());


        /*
        for (int tocados = 0; tocados < simonList.Count; tocados++)
        {
            myAudioSource.clip = aClips[simonList.];
            myAudioSource.Play();

            yield return new WaitForSeconds(1);

            print("Ha sonado \n");
        }*/

        foreach (int sound in simonList)
        {
            myAudioSource.clip = aClips[sound];
            myAudioSource.Play();

            yield return new WaitForSeconds(1);
        }
        //StartCoroutine(STurn());
        
    }

    private void SimonTurn()
    {
        while (simonWaitTimer > 0)
        {
            simonWaitTimer -= Time.deltaTime;
        }
        simonWaitTimer = SimonTimeBetween;

        touchActivated = false;
        System.Random rnd = new System.Random();
        int random = rnd.Next(0,3);
        //int random = UnityEngine.Random.Range(0, 3);

        simonList.Add(random);

        //reproducimos la simon list y al finalizar devolvemos el control al jugador

        PlaySimonList();

        //touchActivated = true;
        /*

        //per al debug
        print("\n");
        print("Jugada: "+ simonList.Count +"\n");
        foreach (int color in simonList)
        {
            Console.WriteLine(color);
        }
        */
        SimonTurn();
        //PlayerTurn();
    }

    private void PlayerTurn()
    {
        while (timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }
        timer = timeBetween;
        print("\nPlayer ha jugado\n");
        SimonTurn();

    }

    public void PlaySimonList()
    {
        
        for (int tocados = 0; tocados < simonList.Count; tocados++) {
            while (playSoundTimer > 0.0f)
            {
                playSoundTimer -= Time.deltaTime;
            }
            playSoundTimer = timeBetweenSounds;

            myAudioSource.clip = aClips[0];
            myAudioSource.Play();
            
            print("Ha sonado \n");
        }
        //yield return new WaitForSeconds(1);
    }

    private void PlayerMove(int move)
    {

        playerList.Add(move);

        if (simonList.Count == playerList.Count )
        {
            int contador = 0;

            while (contador< simonList.Count)
            {
                if (simonList.IndexOf(contador) != playerList.IndexOf(contador))
                {
                    GameOver();
                    return;
                }
            }
            playerList.Clear();
            SimonTurn();

            return;
        }

    }

    private void GameOver()
    {
        print("GameOver");
    }
    
    IEnumerator StartSimon() //NO HO ESTIC UTILITZANT
    {
        textButtonStart.SetActive(false);
        buttonStart.SetActive(false);


        while (!gameOver)
        {
            contador++;
            //no acaba de ser molt random aço :(
            //por ser que el 0 no estiga en aClips?
            simonList.Add(UnityEngine.Random.Range(0,3)); //tinc que dirli unity engine que sino se lia el cerdo
            //print(Time.time);
            yield return new WaitForSeconds(1);
            //PlaySimonList();

            for (int i = 0; i < simonList.Count; i++)
            {
                myAudioSource.clip = aClips[i];
                myAudioSource.Play();
                yield return new WaitForSeconds(1);
            }
            //print(Time.time);
            //myAudioSource.clip = aClips[3];
            //myAudioSource.Play();
            //blueRenderer.material.color = blueColorTrans;
            yield return new WaitForSeconds(1); //mirar de llevar pauses
            //blueRenderer.material.color = blueColor;
            //TURNO DEL JUGADOR (llamar otra coroutine o funcion i hacer otro yield?)
            /*if (contador == 3)
            {
                gameOver = true;
                textButtonStart.SetActive(true);
                buttonStart.SetActive(true);

            }*/
        }
        gameOver = false;


        //yield return null;
    }
    


    //yield WaitForSeconds(0.25);


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

    /*
    void Awake()
    {
        StartCoroutine(Do());
    }

    IEnumerator Do()
    {
        Debug.Log("This will show instantly");
        yield return new WaitForSeconds(2);
        Debug.Log("This will print after 2 seconds");
    }
    */

}
