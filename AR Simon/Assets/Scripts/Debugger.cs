using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Debugger : MonoBehaviour
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
/*
    private VirtualButtonBehaviour redBehaviour;
    private VirtualButtonBehaviour yellowBehaviour;
    private VirtualButtonBehaviour blueBehaviour;
    private VirtualButtonBehaviour greenBehaviour;
*/
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


    void Start()
    {
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



        gameOver = false;



        myAudioSource = GetComponent<AudioSource>();

    }






    void Update()
    {


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
        int random = rnd.Next(0, 3);
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

        for (int tocados = 0; tocados < simonList.Count; tocados++)
        {
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

        if (simonList.Count == playerList.Count)
        {
            int contador = 0;

            while (contador < simonList.Count)
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



}
